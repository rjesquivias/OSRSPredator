using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Application.DTOs;

namespace Application.ItemDetails
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ItemDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ItemDto>>>
        {
            private readonly DataContext context;
            private readonly ILogger<Handler> logger;
            private readonly IMapper mapper;

            public Handler(DataContext context, ILogger<Handler> logger, IMapper mapper)
            {
                this.context = context;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Result<PagedList<ItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = PaginationValidator.Validate(request.Params.PageSize, request.Params.PageNumber);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the PaginationValidator");
                    return Result<PagedList<ItemDto>>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                var query = context.ItemDetails
                    .OrderBy(item => item.name)
                    .ProjectTo<ItemDto>(mapper.ConfigurationProvider)
                    .AsQueryable();

                var itemsToReturn = await PagedList<ItemDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);
                itemsToReturn.ForEach(async item => {
                    if(item.mostRecentSnapshot == null) 
                        item.mostRecentSnapshot = await GetMostRecentSnapshotByItemId(context, item.Id);
                });

                return Result<PagedList<ItemDto>>.Success(itemsToReturn, StatusCodes.Status200OK);
            }

            private async Task<SnapshotDTO> GetMostRecentSnapshotByItemId(DataContext context, long itemDetailsId)
            {
                var itemDetailsDBEntry = await context.ItemDetails.FindAsync(itemDetailsId);
                context.Entry(itemDetailsDBEntry).State = EntityState.Detached;
                var itemDetails = context.ItemDetails.Include(itemDetails => itemDetails.ItemHistoricalList).ThenInclude(itemHistorical => itemHistorical.ItemPriceSnapshot).FirstOrDefault(item => item.Id == itemDetailsId);
                if (itemDetails != null && itemDetails.ItemHistoricalList != null)
                {
                    itemDetails.ItemHistoricalList.ToList().Sort((ItemHistoricalList a, ItemHistoricalList b) =>
                    {
                        var A = a.ItemPriceSnapshot.highTime > a.ItemPriceSnapshot.lowTime ? a.ItemPriceSnapshot.highTime : a.ItemPriceSnapshot.lowTime;
                        var B = b.ItemPriceSnapshot.highTime > b.ItemPriceSnapshot.lowTime ? b.ItemPriceSnapshot.highTime : b.ItemPriceSnapshot.lowTime;
                        return A.CompareTo(B);
                    });

                    var snapshot = itemDetails.ItemHistoricalList.First().ItemPriceSnapshot;
                    return new SnapshotDTO
                    {
                        Id = snapshot.Id,
                        high = snapshot.high,
                        highTime = snapshot.highTime,
                        low = snapshot.low,
                        lowTime = snapshot.lowTime
                    };
                }

                return null;
            }
        }
    }
}

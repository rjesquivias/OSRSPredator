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
                    .Include(item => item.UserWatchList)
                    .ThenInclude(u => u.AppUser)
                    .Include(item => item.UserWatchList)
                    .ThenInclude(u => u.MostRecentSnapshot)
                    .AsQueryable();

                var results = await PagedList<Domain.ItemDetails>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);
                var itemsToReturn = mapper.Map<PagedList<ItemDto>>(results);
                itemsToReturn.ForEach(async item => {
                    if(item.mostRecentSnapshot == null) 
                        item.mostRecentSnapshot = await GetMostRecentSnapshotByItemId(context, item.Id);
                });

                return Result<PagedList<ItemDto>>.Success(itemsToReturn, StatusCodes.Status200OK);
            }

            private async Task<ItemPriceSnapshot> GetMostRecentSnapshotByItemId(DataContext context, long itemDetailsId)
            {
                var itemDetailsDBEntry = await context.ItemDetails.FindAsync(itemDetailsId);
                context.Entry(itemDetailsDBEntry).State = EntityState.Detached;
                var itemHistorical = await context.ItemHistoricals.Include(itemHistoricals => itemHistoricals.historical).FirstOrDefaultAsync(x => x.Id == itemDetailsId);
                if (itemHistorical != null && itemHistorical.historical != null)
                {
                    itemHistorical.historical.Sort((ItemPriceSnapshot a, ItemPriceSnapshot b) =>
                    {
                        var A = a.highTime > a.lowTime ? a.highTime : a.lowTime;
                        var B = b.highTime > b.lowTime ? b.highTime : b.lowTime;
                        return A.CompareTo(B);
                    });

                    return itemHistorical.historical.First();
                }

                return null;
            }
        }
    }
}

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
        public class Query : IRequest<Result<List<ItemDto>>>
        {
            public int pageSize  { get; set; }

            public int page { get; set;}
        }

        public class Handler : IRequestHandler<Query, Result<List<ItemDto>>>
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

            public async Task<Result<List<ItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = PaginationValidator.Validate(request.pageSize, request.page);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the PaginationValidator");
                    return Result<List<ItemDto>>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                List<Domain.ItemDetails> results = await context.ItemDetails
                    .Include(item => item.UserWatchList)
                    .ThenInclude(u => u.AppUser)
                    .Include(item => item.UserWatchList)
                    .ThenInclude(u => u.MostRecentSnapshot)
                    .ToListAsync();

                if(results.Count > request.pageSize)
                    results = results.GetRange((request.page - 1) * request.pageSize, request.pageSize);

                var itemsToReturn = mapper.Map<List<ItemDto>>(results);
                itemsToReturn.ForEach(async item => {
                    if(item.mostRecentSnapshot == null) 
                        item.mostRecentSnapshot = await GetMostRecentSnapshotByItemId(context, item.Id);
                });

                return Result<List<ItemDto>>.Success(itemsToReturn, StatusCodes.Status200OK);
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

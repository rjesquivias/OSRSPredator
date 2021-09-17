using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Domain;
using System.Collections.Generic;
using Application.DTOs;
using System;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Application.Config;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemPriceSnapshots
{
    public class Sync
    {
        public class Command : IRequest<Result<Unit>>
        {
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;

            private readonly ILogger<Handler> logger;

            private readonly IMediator mediator;

            private readonly RsWikiConfig rsWikiConfig;

            public Handler(DataContext context, ILogger<Handler> logger, IMediator mediator, IOptionsMonitor<RsWikiConfig> rsWikiConfig)
            {
                this.context = context;
                this.logger = logger;
                this.mediator = mediator;
                this.rsWikiConfig = rsWikiConfig.CurrentValue;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(rsWikiConfig.ItemPriceSnapshotsUrl))
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        ItemPriceSnapshotDTO dto = JsonConvert.DeserializeObject<ItemPriceSnapshotDTO>(data);
                        foreach (KeyValuePair<string, PriceSnapshot> entry in dto.data)
                        {
                            long itemId = long.Parse(entry.Key);
                            try
                            {
                                // Create the new Snapshot from the DTO we read from the endpoint
                                ItemPriceSnapshot newItemPriceSnapshot = new ItemPriceSnapshot
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    high = entry.Value.high ?? 0,
                                    highTime = entry.Value.highTime ?? 0,
                                    low = entry.Value.low ?? 0,
                                    lowTime = entry.Value.lowTime ?? 0,
                                };
                            
                                // Find the ItemDetails associated with this specific items snapshot
                                var itemDetails = await context.ItemDetails.Include(itemDetails => itemDetails.ItemHistoricalList).FirstOrDefaultAsync(item => item.Id == itemId);

                                // Using the ItemDetails & ItemPriceSnapshot, construct the many to many
                                // ItemHistoricalList object
                                var itemHistoricalItem = new ItemHistoricalList
                                {
                                    ItemPriceSnapshot = newItemPriceSnapshot,
                                    ItemDetails = itemDetails
                                };
                                
                                // Create the associated links
                                if(itemDetails.ItemHistoricalList == null)
                                    itemDetails.ItemHistoricalList = new List<ItemHistoricalList>();
                                itemDetails.ItemHistoricalList.Add(itemHistoricalItem);

                                if(newItemPriceSnapshot.ItemHistoricalList == null)
                                    newItemPriceSnapshot.ItemHistoricalList = new List<ItemHistoricalList>();
                                newItemPriceSnapshot.ItemHistoricalList.Add(itemHistoricalItem);

                                context.ItemHistoricals.Add(itemHistoricalItem);

                                context.SaveChanges();
                            }
                            catch(Exception e)
                            {
                                logger.LogError("Caught exception on id " + itemId + " : " + e.ToString());
                            }
                        }
                    }
                }

                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }
        }
    }
}

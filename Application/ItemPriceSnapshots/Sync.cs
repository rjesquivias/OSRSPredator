using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Domain;
using System.Collections.Generic;
using Application.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemPriceSnapshots
{
    public class Sync
    {
        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            private readonly ILogger<Handler> logger;

            private readonly IMediator mediator;

            public Handler(DataContext context, ILogger<Handler> logger, IMediator mediator)
            {
                this.context = context;
                this.logger = logger;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://prices.runescape.wiki/api/v1/osrs/latest"))
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        ItemPriceSnapshotDTO dto = JsonConvert.DeserializeObject<ItemPriceSnapshotDTO>(data);
                        foreach (KeyValuePair<string, PriceSnapshot> entry in dto.data)
                        {
                            long historicalId = long.Parse(entry.Key);

                            ItemPriceSnapshot newItemPriceSnapshot = new ItemPriceSnapshot
                            {
                                Id = Guid.NewGuid(),
                                high = entry.Value.high ?? 0,
                                highTime = entry.Value.highTime ?? 0,
                                low = entry.Value.low ?? 0,
                                lowTime = entry.Value.lowTime ?? 0,
                            };

                            ItemHistorical itemHistorical = context.ItemHistoricals.Include(i => i.historical).FirstOrDefault(x => x.Id == historicalId);

                            if(itemHistorical != null)
                            {
                                if (itemHistorical.historical == null)
                                {
                                    itemHistorical.historical = new List<ItemPriceSnapshot>();
                                }
                                itemHistorical.historical.Add(newItemPriceSnapshot);
                                try
                                {
                                    context.ItemHistoricals.Update(itemHistorical);
                                    context.ItemPriceSnapshots.Add(newItemPriceSnapshot);
                                }
                                catch(Exception e)
                                {
                                    logger.LogError("Caught exception on id " + historicalId + " : " + e.ToString());
                                }
                            }
                            else
                            {
                                itemHistorical = new ItemHistorical
                                {
                                    Id = historicalId,
                                    historical = new List<ItemPriceSnapshot>
                                    {
                                        newItemPriceSnapshot
                                    }
                                };

                                try
                                {
                                    context.ItemHistoricals.Add(itemHistorical);
                                    context.ItemPriceSnapshots.Add(newItemPriceSnapshot);
                                }
                                catch (Exception e)
                                {
                                    logger.LogError("Caught exception on id " + historicalId + " : " + e.ToString());
                                }
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            logger.LogError("Caught exception: " + e.ToString());
                        }
                    }
                }
                return Unit.Value;
            }
        }
    }
}

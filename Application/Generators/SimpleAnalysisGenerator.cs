using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Generators
{
    public class SimpleAnalysisGenerator : IGenerator
    {
        private readonly DataContext context;
        private readonly IMediator mediator;

        public SimpleAnalysisGenerator(DataContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<List<SimpleItemAnalysis>> Generate()
        {
            List<ItemHistorical> mostRecentSnapshotsForAllItems = getMostRecentSnapshotsForAllItems();
            mostRecentSnapshotsForAllItems.Sort((a, b) =>
            {
                ItemPriceSnapshot aSnapshot = a.historical.FirstOrDefault();
                ItemPriceSnapshot bSnapshot = b.historical.FirstOrDefault();
                return (aSnapshot.high - aSnapshot.low).CompareTo(bSnapshot.high - bSnapshot.low);
            });

            DateTime foo = DateTime.Now;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            long secondsInDay = 86400;
            return await fromItemHistoricals(mostRecentSnapshotsForAllItems.Where(snapshot => unixTime - snapshot.historical.FirstOrDefault().highTime < secondsInDay && unixTime - snapshot.historical.FirstOrDefault().lowTime < secondsInDay).Take(100));
        }

        public List<ItemHistorical> getMostRecentSnapshotsForAllItems()
        {
            List<ItemHistorical> itemHistorical = context.ItemHistoricals.Select(item => new ItemHistorical
            {
                Id = item.Id,
                historical = new List<ItemPriceSnapshot>{
                    item.historical.OrderByDescending(itemPriceSnapshot => itemPriceSnapshot.highTime > itemPriceSnapshot.lowTime ? itemPriceSnapshot.highTime : itemPriceSnapshot.lowTime).FirstOrDefault()
                }
            })
            .ToList();

            return itemHistorical;
        }

        public async Task<List<SimpleItemAnalysis>> fromItemHistoricals(IEnumerable<ItemHistorical> itemHistoricals)
        {
            List<SimpleItemAnalysis> simpleItemAnalysisList = new List<SimpleItemAnalysis>();

            foreach (ItemHistorical item in itemHistoricals)
            {
                ItemDetail itemDetail = await mediator.Send(new Application.ItemDetails.Details.Query { Id = item.Id });

                simpleItemAnalysisList.Add(new SimpleItemAnalysis
                {
                    delta = item.historical.FirstOrDefault().high - item.historical.FirstOrDefault().low,
                    mostRecentSnapshot = item.historical.FirstOrDefault(),
                    itemDetails = itemDetail,
                    prediction = 999
                });
            }

            return simpleItemAnalysisList;
        }
    }
}

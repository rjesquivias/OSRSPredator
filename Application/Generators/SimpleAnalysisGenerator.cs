using Application.Mapping;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Generators
{
    public interface ISimpleAnalysisGenerator : IGenerator
    {    
    }

    public class SimpleAnalysisGenerator : ISimpleAnalysisGenerator
    {
        private readonly DataContext context;

        private readonly IMediator mediator;

        public SimpleAnalysisGenerator(DataContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }


        public async Task<List<Domain.SimpleItemAnalysis>> Generate(int pageSize, int page)
        {
            List<ItemHistorical> mostRecentSnapshotsForAllItems = getMostRecentSnapshotsForAllItems();
            mostRecentSnapshotsForAllItems.Sort((a, b) =>
            {
                ItemPriceSnapshot aSnapshot = a.historical.FirstOrDefault();
                ItemPriceSnapshot bSnapshot = b.historical.FirstOrDefault();

                float aDelta = aSnapshot.high - aSnapshot.low;
                float bDelta = bSnapshot.high - bSnapshot.low;
                return bDelta.CompareTo(aDelta);
            });

            SimpleItemAnalysisMap map = new SimpleItemAnalysisMap(this.mediator);
            return await map.fromItemHistoricals(mostRecentSnapshotsForAllItems.GetRange((page - 1) * pageSize, pageSize));
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
    }
}

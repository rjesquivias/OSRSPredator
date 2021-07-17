using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Mapping
{
    public class SimpleItemAnalysisMap
    {
        private readonly IMediator mediator;

        public SimpleItemAnalysisMap(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<Domain.SimpleItemAnalysis>> fromItemHistoricals(IEnumerable<ItemHistorical> itemHistoricals)
        {
            List<Domain.SimpleItemAnalysis> simpleItemAnalysisList = new List<Domain.SimpleItemAnalysis>();

            foreach (ItemHistorical item in itemHistoricals)
            {
                ItemDetail itemDetail = await mediator.Send(new Application.ItemDetails.Details.Query { Id = item.Id });

                simpleItemAnalysisList.Add(new Domain.SimpleItemAnalysis
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
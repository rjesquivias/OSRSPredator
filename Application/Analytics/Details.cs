using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Analytics
{
    public class Details
    {
        public class Query : IRequest<ItemAnalysis>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ItemAnalysis>
        {
            private readonly DataContext context;

            private readonly IMediator mediator;

            public Handler(DataContext context, IMediator mediator)
            {
                this.context = context;
                this.mediator = mediator;
            }

            public async Task<ItemAnalysis> Handle(Query request, CancellationToken cancellationToken)
            {
                ItemPriceSnapshot snapshot = await GetMostRecentSnapshot(request.Id);
                ItemDetail itemDetail = await mediator.Send(new Application.ItemDetails.Details.Query { Id = request.Id });
                List<ItemPriceSnapshot> snapshotGraph = await GetSnapshotGraph(request.Id, 24);

                long delta = snapshot != null ? snapshot.high - snapshot.low : 0;

                return new ItemAnalysis
                {
                    delta = delta,
                    mostRecentSnapshot = snapshot,
                    itemDetails = itemDetail,
                    snapshotGraph = snapshotGraph,
                    prediction = 4000
                };
            }

            public async Task<ItemPriceSnapshot> GetMostRecentSnapshot(long id)
            {
                ItemHistorical itemHistorical = await context.ItemHistoricals.Include(i => i.historical).FirstOrDefaultAsync(i => i.Id == id);
                ItemPriceSnapshot mostRecentSnapshot = null;
                long mostRecentUnixTime = 0;
                if (itemHistorical == null) return null;
                foreach (ItemPriceSnapshot snapshot in itemHistorical.historical)
                {
                    if(mostRecentSnapshot == null)
                    {
                        mostRecentSnapshot = snapshot;
                        mostRecentUnixTime = snapshot.highTime > snapshot.lowTime ? snapshot.highTime : snapshot.lowTime;
                    }
                    else
                    {
                        if(snapshot.highTime > mostRecentUnixTime || snapshot.lowTime > mostRecentUnixTime)
                        {
                            mostRecentUnixTime = snapshot.highTime > snapshot.lowTime ? snapshot.highTime : snapshot.lowTime;
                            mostRecentSnapshot = snapshot;
                        }
                    }
                }

                return mostRecentSnapshot;
            }

            public async Task<List<ItemPriceSnapshot>> GetSnapshotGraph(long id, int lastNHours)
            {
                List<ItemPriceSnapshot> snapshotGraph = new List<ItemPriceSnapshot>();
                ItemHistorical itemHistorical = await context.ItemHistoricals.Include(i => i.historical).FirstOrDefaultAsync(i => i.Id == id);
                if (itemHistorical == null) return null;

                DateTime foo = DateTime.Now;
                long unixTimeNow = ((DateTimeOffset)foo).ToUnixTimeSeconds();
                foreach (ItemPriceSnapshot snapshot in itemHistorical.historical)
                {
                    long mostRecentSnapshotTime = snapshot.highTime > snapshot.lowTime ? snapshot.highTime : snapshot.lowTime;
                    if (isWithinNHours(unixTimeNow, mostRecentSnapshotTime, lastNHours))
                    {
                        snapshotGraph.Add(snapshot);
                    }
                }

                return snapshotGraph;
            }

            public bool isWithinNHours(long unixTimeA, long unixTimeB, int hours)
            {
                long elapsedUnixTime = unixTimeA - unixTimeB;
                long secondsInHour = 3600;

                return elapsedUnixTime <= hours * secondsInHour;
            }
        }
    }
}

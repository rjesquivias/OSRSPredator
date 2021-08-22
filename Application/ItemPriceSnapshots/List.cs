using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemPriceSnapshots
{
    public class List
    {
        public class Query : IRequest<Result<List<ItemPriceSnapshot>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<ItemPriceSnapshot>>>
        {
            private readonly DataContext context;
            private readonly ILogger logger;

            public Handler(DataContext context, ILogger<Handler> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<Result<List<ItemPriceSnapshot>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<ItemPriceSnapshot> results = await Task.Run(() => context.ItemPriceSnapshots.AsEnumerable().OrderBy(snapshot => snapshot, new ItemPriceSnapshotComparer()).ToList());
                return Result<List<ItemPriceSnapshot>>.Success(results, StatusCodes.Status200OK);
            }
        }

        public class ItemPriceSnapshotComparer : IComparer<ItemPriceSnapshot>
        {
            public int Compare(ItemPriceSnapshot a, ItemPriceSnapshot b)
            {
                long aMostRecentTime = a.highTime > a.lowTime ? a.highTime : a.lowTime;
                long bMostRecentTime = b.highTime > b.lowTime ? b.highTime : b.lowTime;
                return aMostRecentTime.CompareTo(bMostRecentTime);
            }
        }
    }
}

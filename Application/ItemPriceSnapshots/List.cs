using Domain;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemPriceSnapshots
{
    public class List
    {
        public class Query : IRequest<List<ItemPriceSnapshot>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ItemPriceSnapshot>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<ItemPriceSnapshot>> Handle(Query request, CancellationToken cancellationToken)
            {
                ItemPriceSnapshotComparer comparer = new ItemPriceSnapshotComparer();
                return context.ItemPriceSnapshots.OrderBy(snapshot => snapshot, comparer).ToList();
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

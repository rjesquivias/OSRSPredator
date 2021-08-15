using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.WatchList
{
    public class List
    {
        public class Query : IRequest<List<Domain.WatchListItemDetails>>
        {
            public int pageSize  { get; set; }

            public int page { get; set;}
        }

        public class Handler : IRequestHandler<Query, List<Domain.WatchListItemDetails>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<Domain.WatchListItemDetails>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    List<Domain.WatchListItemDetails> results = context.WatchList.Include(item => item.mostRecentSnapshot).ToList();
                    try {
                        return results.GetRange((request.page - 1) * request.pageSize, request.pageSize);
                    } catch(System.Exception e) {
                        return results;
                    }
                });
            }
        }
    }
}

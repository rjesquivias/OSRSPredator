using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemDetails
{
    public class List
    {
        public class Query : IRequest<List<Domain.DefaultItemDetails>>
        {
            public int pageSize  { get; set; }

            public int page { get; set;}
        }

        public class Handler : IRequestHandler<Query, List<Domain.DefaultItemDetails>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<Domain.DefaultItemDetails>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => context.ItemDetails.Include(item => item.mostRecentSnapshot).ToList().GetRange((request.page - 1) * request.pageSize, request.pageSize));
            }
        }
    }
}

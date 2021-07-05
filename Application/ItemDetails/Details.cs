using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemDetails
{
    public class Details
    {
        public class Query : IRequest<ItemDetail>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ItemDetail>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<ItemDetail> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.ItemDetails.FindAsync(request.Id);
            }

        }
    }
}

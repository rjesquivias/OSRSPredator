using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemPriceSnapshots
{
    public class Details
    {
        public class Query : IRequest<ItemPriceSnapshot>
        {
            public String Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ItemPriceSnapshot>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<ItemPriceSnapshot> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.ItemPriceSnapshots.FindAsync(request.Id);
            }

        }
    }
}

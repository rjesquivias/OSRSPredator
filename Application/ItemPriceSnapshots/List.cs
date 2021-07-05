using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return await context.ItemPriceSnapshots.ToListAsync(cancellationToken);
            }
        }
    }
}

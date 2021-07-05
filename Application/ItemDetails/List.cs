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

namespace Application.ItemDetails
{
    public class List
    {
        public class Query : IRequest<List<ItemDetail>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ItemDetail>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<ItemDetail>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.ItemDetails.ToListAsync(cancellationToken);
            }
        }
    }
}

using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemHistoricals
{
    public class List
    {
        public class Query : IRequest<List<ItemHistorical>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ItemHistorical>>
        {
            private readonly DataContext context;
            private readonly IMediator mediator;

            public Handler(DataContext context, IMediator mediator)
            {
                this.context = context;
                this.mediator = mediator;
            }

            public async Task<List<ItemHistorical>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.ItemHistoricals.Include(item => item.historical).ToListAsync(cancellationToken);
            }
        }
    }
}
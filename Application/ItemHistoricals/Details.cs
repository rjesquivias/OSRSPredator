using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemHistoricals
{
    public class Details
    {
        public class Query : IRequest<Result<ItemHistorical>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ItemHistorical>>
        {
            private readonly DataContext context;
            private readonly IMediator mediator;

            public Handler(DataContext context, IMediator mediator)
            {
                this.context = context;
                this.mediator = mediator;
            }

            public async Task<Result<ItemHistorical>> Handle(Query request, CancellationToken cancellationToken)
            {
                var itemHistorical = await context.ItemHistoricals.Include(i => i.historical).FirstOrDefaultAsync(i => i.Id == request.Id);

                return Result<ItemHistorical>.Success(itemHistorical);
            }
        }
    }
}

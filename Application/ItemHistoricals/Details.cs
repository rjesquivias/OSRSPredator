using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

            private readonly ILogger logger;

            public Handler(DataContext context, IMediator mediator, ILogger<Handler> logger)
            {
                this.context = context;
                this.mediator = mediator;
                this.logger = logger;
            }

            public async Task<Result<ItemHistorical>> Handle(Query request, CancellationToken cancellationToken)
            {
                var itemHistorical = await context.ItemHistoricals.Include(i => i.historical).FirstOrDefaultAsync(i => i.Id == request.Id);
                if(itemHistorical == null)
                {
                    logger.LogInformation($"ItemHistorical with the id of {request.Id} does not exist");
                    return null;
                }

                return Result<ItemHistorical>.Success(itemHistorical);
            }
        }
    }
}

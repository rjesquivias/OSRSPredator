using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemHistoricals
{
    public class List
    {
        public class Query : IRequest<Result<List<ItemHistorical>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<ItemHistorical>>>
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

            public async Task<Result<List<ItemHistorical>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<ItemHistorical> results = await context.ItemHistoricals.Include(item => item.historical).ToListAsync(cancellationToken);
                return Result<List<ItemHistorical>>.Success(results, StatusCodes.Status200OK);
            }
        }
    }
}
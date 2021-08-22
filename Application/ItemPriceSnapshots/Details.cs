using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemPriceSnapshots
{
    public class Details
    {
        public class Query : IRequest<Result<ItemPriceSnapshot>>
        {
            public String Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ItemPriceSnapshot>>
        {
            private readonly DataContext context;
            private readonly ILogger logger;

            public Handler(DataContext context, ILogger<Handler> logger)
            {
                this.logger = logger;
                this.context = context;
            }

            public async Task<Result<ItemPriceSnapshot>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = GuidValidator.Validate(request.Id);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the PaginationValidator");
                    return Result<ItemPriceSnapshot>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                var itemPriceSnapshot = await context.ItemPriceSnapshots.FindAsync(request.Id);
                if (itemPriceSnapshot == null)
                {
                    logger.LogInformation($"ItemPriceSnapshot with the id of {request.Id} does not exist");
                    return null;
                }

                return Result<ItemPriceSnapshot>.Success(itemPriceSnapshot);
            }

        }
    }
}

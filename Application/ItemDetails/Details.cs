﻿using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Application.ItemDetails
{
    public class Details
    {
        public class Query : IRequest<Result<Domain.ItemDetails>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Domain.ItemDetails>>
        {
            private readonly DataContext context;
            private readonly ILogger<Handler> logger;

            public Handler(DataContext context, ILogger<Handler> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<Result<Domain.ItemDetails>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = IdValidator.Validate(request.Id);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the IdValidator");
                    return Result<Domain.ItemDetails>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                var entity = await context.ItemDetails.FindAsync(request.Id);
                if(entity == null)
                {
                    logger.LogInformation($"DefaultItemDetails with the id of {request.Id} was not found");
                    return null;
                } 

                return Result<Domain.ItemDetails>.Success(entity);
            }

        }
    }
}

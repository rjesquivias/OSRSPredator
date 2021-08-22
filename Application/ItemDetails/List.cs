using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Application.ItemDetails
{
    public class List
    {
        public class Query : IRequest<Result<List<DefaultItemDetails>>>
        {
            public int pageSize  { get; set; }

            public int page { get; set;}
        }

        public class Handler : IRequestHandler<Query, Result<List<DefaultItemDetails>>>
        {
            private readonly DataContext context;
            private readonly ILogger<Handler> logger;

            public Handler(DataContext context, ILogger<Handler> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<Result<List<DefaultItemDetails>>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = PaginationValidator.Validate(request.pageSize, request.page);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the PaginationValidator");
                    return Result<List<DefaultItemDetails>>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                List<Domain.DefaultItemDetails> results = await context.ItemDetails.Include(item => item.mostRecentSnapshot).ToListAsync();
                if(results.Count > request.pageSize)
                    results = results.GetRange((request.page - 1) * request.pageSize, request.pageSize);

                return Result<List<DefaultItemDetails>>.Success(results, StatusCodes.Status200OK);
            }
        }
    }
}

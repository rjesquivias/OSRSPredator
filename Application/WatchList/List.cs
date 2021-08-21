using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Application.Core;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.WatchList
{
    public class List
    {
        public class Query : IRequest<Result<List<WatchListItemDetails>>>
        {
            public int pageSize  { get; set; }

            public int page { get; set;}
        }

        public class Handler : IRequestHandler<Query, Result<List<WatchListItemDetails>>>
        {
            private readonly DataContext context;

            private readonly ILogger logger;

            public Handler(DataContext context, ILogger<Handler> logger)
            {
                this.context = context;
                this.logger = logger;
            }

            public async Task<Result<List<WatchListItemDetails>>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = PaginationValidator.Validate(request.pageSize, request.page);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the PaginationValidator");
                    return Result<List<WatchListItemDetails>>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                List<Domain.WatchListItemDetails> results = await context.WatchList.Include(item => item.mostRecentSnapshot).ToListAsync();
                try {
                    if(results.Count > request.pageSize)
                        results = results.GetRange((request.page - 1) * request.pageSize, request.pageSize);
                } catch(System.Exception e) {
                    logger.LogError(e, e.Message);
                    return Result<List<WatchListItemDetails>>.Failure(e.Message, StatusCodes.Status500InternalServerError);
                }

                throw new System.Exception("Random exception called");

                return Result<List<WatchListItemDetails>>.Success(results, StatusCodes.Status200OK);
            }
        }
    }
}

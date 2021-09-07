using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using System.Linq;

namespace Application.WatchList
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<Domain.ItemDetails>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<Domain.ItemDetails>>>
        {
            private readonly DataContext context;

            private readonly ILogger logger;
            private readonly IUsernameAccessor usernameAccessor;

            public Handler(DataContext context, ILogger<Handler> logger, IUsernameAccessor usernameAccessor)
            {
                this.context = context;
                this.logger = logger;
                this.usernameAccessor = usernameAccessor;
            }

            public async Task<Result<PagedList<Domain.ItemDetails>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == usernameAccessor.GetUsername());

                Dictionary<string, string[]> errors = PaginationValidator.Validate(request.Params.PageSize, request.Params.PageNumber);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the PaginationValidator");
                    return Result<PagedList<Domain.ItemDetails>>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                var query = context.UserWatchList.Where(item => item.AppUserId == user.Id).Select(item => item.ItemDetails).AsQueryable();
                var results = await PagedList<Domain.ItemDetails>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);

                return Result<PagedList<Domain.ItemDetails>>.Success(results, StatusCodes.Status200OK);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.WatchList
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.ItemDetails itemDetails { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(command => command.itemDetails).SetValidator(new WatchListValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;

            private readonly IMediator mediator;

            private readonly ILogger logger;

            private readonly IUsernameAccessor usernameAccessor;

            public Handler(DataContext context, IMediator mediator, ILogger<Handler> logger, IUsernameAccessor usernameAccessor)
            {
                this.context = context;
                this.mediator = mediator;
                this.logger = logger;
                this.usernameAccessor = usernameAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {          
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == usernameAccessor.GetUsername());

                var entity = await context.UserWatchList.FindAsync(user.Id, request.itemDetails.Id);
                if(entity == null)
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} was not found");
                    return null;
                } 

                context.Remove(entity);
                var result = await context.SaveChangesAsync() > 0;
                if(!result)
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} was not successfully removed from the database");
                    return Result<Unit>.Failure("Failed to delete itemDetails", StatusCodes.Status500InternalServerError);
                }

                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.WatchList
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Domain.WatchListItemDetails itemDetails { get; set; }
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

            public Handler(DataContext context, IMediator mediator, ILogger<Handler> logger)
            {
                this.context = context;
                this.mediator = mediator;
                this.logger = logger;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {             
                var entity = await context.WatchList.FindAsync(request.itemDetails.Id);
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
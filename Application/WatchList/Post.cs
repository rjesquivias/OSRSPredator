using System.Linq;
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
     public class Post
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
                if(context.WatchList.Any(item => item.Id == request.itemDetails.Id)) 
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} already exists within the watchlist");
                    return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
                }

                var mostRecentSnapshot = await context.ItemPriceSnapshots.FindAsync(request.itemDetails.mostRecentSnapshot.Id);
                if(mostRecentSnapshot == null) 
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} does not contain any snapshot data");
                    logger.LogInformation($"ItemPriceSnapshot with the id of {request.itemDetails.mostRecentSnapshot.Id} does not exist within the ItemPriceSnapshots table");
                }

                request.itemDetails.mostRecentSnapshot = mostRecentSnapshot;
                await context.WatchList.AddAsync(request.itemDetails);
                var result = await context.SaveChangesAsync() > 0;

                if(!result)
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} was not successfully added to the database");
                    return Result<Unit>.Failure("Failed to write itemDetails", StatusCodes.Status500InternalServerError);
                } 

                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }
        }
    }
}
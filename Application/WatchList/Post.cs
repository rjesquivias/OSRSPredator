using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.WatchList
{
     public class Post
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
            private readonly ILogger logger;
            private readonly IUsernameAccessor usernameAccessor;

            public Handler(DataContext context, ILogger<Handler> logger, IUsernameAccessor usernameAccessor)
            {
                this.context = context;
                this.logger = logger;
                this.usernameAccessor = usernameAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == usernameAccessor.GetUsername());
                if(context.UserWatchList.Any(item => item.AppUserId == user.Id && item.ItemDetailsId == request.itemDetails.Id)) 
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} already exists within the watchlist");
                    return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
                }

                var userWatchListItem = new UserWatchList
                {
                    AppUser = user,
                    ItemDetails = request.itemDetails,
                    MostRecentSnapshot = await GetMostRecentSnapshotByItemId(context, request.itemDetails.Id)
                };

                request.itemDetails.UserWatchList.Add(userWatchListItem);
                context.UserWatchList.Add(userWatchListItem);
                context.ItemDetails.Attach(request.itemDetails);

                var result = await context.SaveChangesAsync() > 0;
                if(!result)
                {
                    logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} was not successfully added to the database");
                    return Result<Unit>.Failure("Failed to write itemDetails", StatusCodes.Status500InternalServerError);
                } 

                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }

            public async Task<ItemPriceSnapshot> GetMostRecentSnapshotByItemId(DataContext context, long itemDetailsId)
            {
                var itemDetailsDBEntry = await context.ItemDetails.FindAsync(itemDetailsId);
                context.Entry(itemDetailsDBEntry).State = EntityState.Detached;
                var itemHistorical = await context.ItemHistoricals.Include(itemHistoricals => itemHistoricals.historical).FirstOrDefaultAsync(x => x.Id == itemDetailsId);
                if (itemHistorical != null && itemHistorical.historical != null)
                {
                    itemHistorical.historical.Sort((ItemPriceSnapshot a, ItemPriceSnapshot b) =>
                    {
                        var A = a.highTime > a.lowTime ? a.highTime : a.lowTime;
                        var B = b.highTime > b.lowTime ? b.highTime : b.lowTime;
                        return A.CompareTo(B);
                    });

                    return itemHistorical.historical.First();
                }

                return null;
            }
        }
    }
}
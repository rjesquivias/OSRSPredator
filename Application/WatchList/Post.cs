using System;
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

                try
                {
                    var result = await context.SaveChangesAsync() > 0;
                    if(!result)
                    {
                        logger.LogInformation($"WatchListItemDetails with the id of {request.itemDetails.Id} was not successfully added to the database");
                        return Result<Unit>.Failure("Failed to write itemDetails", StatusCodes.Status500InternalServerError);
                    } 
                }
                catch(Exception e)
                {
                    throw e.InnerException;
                }

                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }

            public async Task<ItemPriceSnapshot> GetMostRecentSnapshotByItemId(DataContext context, long itemDetailsId)
            {
                var itemDetailsDBEntry = await context.ItemDetails.FindAsync(itemDetailsId);
                context.Entry(itemDetailsDBEntry).State = EntityState.Detached;
                var itemDetails = await context.ItemDetails.Include(itemDetails => itemDetails.ItemHistoricalList).ThenInclude(itemHistorical => itemHistorical.ItemPriceSnapshot).FirstOrDefaultAsync(x => x.Id == itemDetailsId);
                context.Entry(itemDetails).State = EntityState.Detached;
                if (itemDetails != null && itemDetails.ItemHistoricalList != null)
                {
                    itemDetails.ItemHistoricalList.ToList().Sort((ItemHistoricalList a, ItemHistoricalList b) =>
                    {
                        var A = a.ItemPriceSnapshot.highTime > a.ItemPriceSnapshot.lowTime ? a.ItemPriceSnapshot.highTime : a.ItemPriceSnapshot.lowTime;
                        var B = b.ItemPriceSnapshot.highTime > b.ItemPriceSnapshot.lowTime ? b.ItemPriceSnapshot.highTime : b.ItemPriceSnapshot.lowTime;
                        return A.CompareTo(B);
                    });

                    return itemDetails.ItemHistoricalList.First().ItemPriceSnapshot;
                }
                return null;
            }
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.WatchList
{
     public class Post
    {
        public class Command : IRequest
        {
            public Domain.WatchListItemDetails itemDetails { get; set; }
        }

        public class CommandValidator : AbstractValidator<Domain.WatchListItemDetails>
        {
            public CommandValidator()
            {
                RuleFor(itemDetails => itemDetails.examine).NotEmpty();
                RuleFor(itemDetails => itemDetails.highalch).NotEmpty();
                RuleFor(itemDetails => itemDetails.icon).NotEmpty();
                RuleFor(itemDetails => itemDetails.Id).NotEmpty();
                RuleFor(itemDetails => itemDetails.limit).NotEmpty();
                RuleFor(itemDetails => itemDetails.lowalch).NotEmpty();
                RuleFor(itemDetails => itemDetails.members).NotEmpty();
                RuleFor(itemDetails => itemDetails.name).NotEmpty();
                RuleFor(itemDetails => itemDetails.value).NotEmpty();
                RuleFor(itemDetails => itemDetails.mostRecentSnapshot).NotEmpty();
                RuleFor(itemDetails => itemDetails.mostRecentSnapshot.high).NotEmpty();
                RuleFor(itemDetails => itemDetails.mostRecentSnapshot.highTime).NotEmpty();
                RuleFor(itemDetails => itemDetails.mostRecentSnapshot.Id).NotEmpty();
                RuleFor(itemDetails => itemDetails.mostRecentSnapshot.low).NotEmpty();
                RuleFor(itemDetails => itemDetails.mostRecentSnapshot.lowTime).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            private readonly IMediator mediator;

            public Handler(DataContext context, IMediator mediator)
            {
                this.context = context;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if(context.WatchList.Any(item => item.Id == request.itemDetails.Id)) return Unit.Value;

                var mostRecentSnapshot = await context.ItemPriceSnapshots.FindAsync(request.itemDetails.mostRecentSnapshot.Id);
                request.itemDetails.mostRecentSnapshot = mostRecentSnapshot;

                context.WatchList.Add(request.itemDetails);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
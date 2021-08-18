using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.WatchList
{
    public class Delete
    {
        public class Command : IRequest
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
                context.WatchList.Remove(request.itemDetails);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
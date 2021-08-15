using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

                await context.WatchList.AddAsync(request.itemDetails);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
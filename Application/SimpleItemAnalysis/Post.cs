using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SimpleItemAnalysis
{
    public class Post
    {
        public class Command : IRequest
        {
            public Domain.SimpleItemAnalysis simpleItemAnalysis { get; set; }
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
                Domain.SimpleItemAnalysis simpleItemAnalysis = request.simpleItemAnalysis;
                simpleItemAnalysis.Id = Application.Util.Random.RandomNumber(int.MinValue, int.MaxValue);

                simpleItemAnalysis.detailsId = simpleItemAnalysis.itemDetails.Id;
                simpleItemAnalysis.snapshotId = simpleItemAnalysis.mostRecentSnapshot.Id;
                simpleItemAnalysis.itemDetails = null;
                simpleItemAnalysis.mostRecentSnapshot = null;

                context.WatchList.Add(simpleItemAnalysis);
                context.SaveChanges();
                return Unit.Value;
            }
        }
    }
}
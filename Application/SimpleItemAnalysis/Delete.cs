using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Persistence;

namespace Application.SimpleItemAnalysis
{
    public class Delete
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
                var item = context.WatchList.Include(i => i.mostRecentSnapshot).Include(i => i.itemDetails).FirstOrDefault(item => item.detailsId == request.simpleItemAnalysis.itemDetails.Id);
                if(item == null) return Unit.Value;

                context.WatchList.Remove(item);

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
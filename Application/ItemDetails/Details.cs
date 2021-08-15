using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemDetails
{
    public class Details
    {
        public class Query : IRequest<Domain.DefaultItemDetails>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Domain.DefaultItemDetails>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Domain.DefaultItemDetails> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.ItemDetails.FindAsync(request.Id);
            }

        }
    }
}

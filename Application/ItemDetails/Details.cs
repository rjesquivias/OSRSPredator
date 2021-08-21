using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Application.Core;

namespace Application.ItemDetails
{
    public class Details
    {
        public class Query : IRequest<Result<DefaultItemDetails>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DefaultItemDetails>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Result<DefaultItemDetails>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<DefaultItemDetails>.Success(await context.ItemDetails.FindAsync(request.Id));
            }

        }
    }
}

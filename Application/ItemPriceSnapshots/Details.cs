using Application.Core;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ItemPriceSnapshots
{
    public class Details
    {
        public class Query : IRequest<Result<ItemPriceSnapshot>>
        {
            public String Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ItemPriceSnapshot>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Result<ItemPriceSnapshot>> Handle(Query request, CancellationToken cancellationToken)
            {
                var itemPriceSnapshot = await context.ItemPriceSnapshots.FindAsync(request.Id);

                return Result<ItemPriceSnapshot>.Success(itemPriceSnapshot);
            }

        }
    }
}

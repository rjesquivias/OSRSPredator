using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.ItemDetails
{
    public class List
    {
        public class Query : IRequest<Result<List<DefaultItemDetails>>>
        {
            public int pageSize  { get; set; }

            public int page { get; set;}
        }

        public class Handler : IRequestHandler<Query, Result<List<DefaultItemDetails>>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Result<List<DefaultItemDetails>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var itemDetails = await Task.Run(() => context.ItemDetails.Include(item => item.mostRecentSnapshot).ToList().GetRange((request.page - 1) * request.pageSize, request.pageSize));

                return Result<List<Domain.DefaultItemDetails>>.Success(itemDetails);
            }
        }
    }
}

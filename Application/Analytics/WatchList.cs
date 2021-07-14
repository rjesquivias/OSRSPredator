using Application.Generators;
using Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Analytics
{
    public class WatchList
    {
        public class Query : IRequest<List<SimpleItemAnalysis>>
        {
            public int pageSize {get; set; }
            public int page { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<SimpleItemAnalysis>>
        {
            private readonly IWatchListGenerator generator;

            public Handler(IWatchListGenerator generator)
            {
                this.generator = generator;
            }
            public async Task<List<SimpleItemAnalysis>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await generator.Generate(request.pageSize, request.page);
            }
        }
    }
}

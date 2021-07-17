using Application.Generators;
using Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Analytics
{
    public class List
    {
        public class Query : IRequest<List<Domain.SimpleItemAnalysis>>
        {
            public int pageSize {get; set; }
            public int page { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Domain.SimpleItemAnalysis>>
        {
            private readonly ISimpleAnalysisGenerator generator;

            public Handler(ISimpleAnalysisGenerator generator)
            {
                this.generator = generator;
            }
            public async Task<List<Domain.SimpleItemAnalysis>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await generator.Generate(request.pageSize, request.page);
            }
        }
    }
}

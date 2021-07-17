using Application.Generators;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SimpleItemAnalysis
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
            private readonly IWatchListGenerator generator;

            public Handler(IWatchListGenerator generator)
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

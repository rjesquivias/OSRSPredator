using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Application.Config;
using Microsoft.Extensions.Options;

namespace Application.ItemDetails
{
    public class Sync
    {
        public class Command : IRequest<Result<Unit>>
        {
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;

            private readonly ILogger<Handler> logger;

            private readonly IMediator mediator;

            private readonly RsWikiConfig rsWikiConfig;

            public Handler(DataContext context, ILogger<Handler> logger, IMediator mediator, IOptionsMonitor<RsWikiConfig> rsWikiConfig)
            {
                this.context = context;
                this.logger = logger;
                this.mediator = mediator;
                this.rsWikiConfig = rsWikiConfig.CurrentValue;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(rsWikiConfig.ItemDetailsUrl))
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        List<Domain.ItemDetails> itemDetails = JsonConvert.DeserializeObject<List<Domain.ItemDetails>>(data);

                        foreach (var item in itemDetails) {
                            var itemDetailsDBEntry = await context.ItemDetails.FindAsync(item.Id);
                            if(itemDetailsDBEntry == null)
                            {
                                await context.ItemDetails.AddAsync(item);
                            }
                            context.SaveChanges();
                        }
                    }
                }
                
                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }
        }
    }
}

using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Domain;
using System.Collections.Generic;
using Application.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemDetails
{
    public class Sync
    {
        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            private readonly ILogger<Handler> logger;

            private readonly IMediator mediator;

            public Handler(DataContext context, ILogger<Handler> logger, IMediator mediator)
            {
                this.context = context;
                this.logger = logger;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://prices.runescape.wiki/api/v1/osrs/mapping"))
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        List<ItemDetail> itemDetails = JsonConvert.DeserializeObject<List<ItemDetail>>(data);
                        foreach (var entity in context.ItemDetails)
                            context.ItemDetails.Remove(entity);
                        context.AddRange(itemDetails);
                        context.SaveChanges();
                    }

                }

                return Unit.Value;
            }
        }
    }
}

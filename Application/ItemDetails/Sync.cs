﻿using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Linq;
using Application.Core;
using Microsoft.AspNetCore.Http;

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

            public Handler(DataContext context, ILogger<Handler> logger, IMediator mediator)
            {
                this.context = context;
                this.logger = logger;
                this.mediator = mediator;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://prices.runescape.wiki/api/v1/osrs/mapping"))
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        List<Domain.ItemDetails> itemDetails = JsonConvert.DeserializeObject<List<Domain.ItemDetails>>(data);

                        foreach (var item in itemDetails) {
                            var itemDetailsDBEntry = await context.ItemDetails.FindAsync(item.Id);
                            var itemHistorical = await context.ItemDetails.Include(itemDetails => itemDetails.ItemHistoricalList).FirstOrDefaultAsync(x => x.Id == item.Id);
                            if (itemHistorical != null && itemHistorical.ItemHistoricalList != null)
                            {
                                itemHistorical.ItemHistoricalList.ToList().Sort((ItemHistoricalList a, ItemHistoricalList b) =>
                                {
                                    var A = a.ItemPriceSnapshot.highTime > a.ItemPriceSnapshot.lowTime ? a.ItemPriceSnapshot.highTime : a.ItemPriceSnapshot.lowTime;
                                    var B = b.ItemPriceSnapshot.highTime > b.ItemPriceSnapshot.lowTime ? b.ItemPriceSnapshot.highTime : b.ItemPriceSnapshot.lowTime;
                                    return A.CompareTo(B);
                                });

                                context.SaveChanges();
                            }
                        }
                    }
                }
                
                return Result<Unit>.Success(Unit.Value, StatusCodes.Status200OK);
            }
        }
    }
}

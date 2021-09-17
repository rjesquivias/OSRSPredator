using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.ItemDetails
{
    public class Details
    {
        public class Query : IRequest<Result<ItemDetailsDTO>>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ItemDetailsDTO>>
        {
            private readonly DataContext context;

            private readonly ILogger<Handler> logger;

            private readonly IMapper mapper;

            public Handler(DataContext context, ILogger<Handler> logger, IMapper mapper)
            {
                this.context = context;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Result<ItemDetailsDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                Dictionary<string, string[]> errors = IdValidator.Validate(request.Id);
                if(errors.Count != 0)
                {
                    logger.LogInformation($"Request failed with the IdValidator");
                    return Result<ItemDetailsDTO>.Failure(errors, StatusCodes.Status400BadRequest);
                }

                var entity = await context.ItemDetails.Include(item => item.ItemHistoricalList).ThenInclude(itemHistorical => itemHistorical.ItemPriceSnapshot).FirstOrDefaultAsync(item => item.Id == request.Id);
                if(entity == null)
                {
                    logger.LogInformation($"DefaultItemDetails with the id of {request.Id} was not found");
                    return null;
                } 

                return Result<ItemDetailsDTO>.Success(mapper.Map<ItemDetailsDTO>(entity));
            }

        }
    }
}

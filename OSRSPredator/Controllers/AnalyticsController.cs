using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Domain;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        //[HttpGet]
        //public async Task<ActionResult<List<ItemDetail>>> GetItemDetails() => await Mediator.Send(new Application.ItemDetails.List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemAnalysis>> GetItemDetail(long id) => await Mediator.Send(new Application.Analytics.Details.Query { Id = id });
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemHistoricalController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<ActionResult<List<ItemHistorical>>> GetItemHistoricals() => await Mediator.Send(new Application.ItemHistoricals.List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemHistorical>> GetItemHistorical(long id) => await Mediator.Send(new Application.ItemHistoricals.Details.Query { Id = id });
    }
}

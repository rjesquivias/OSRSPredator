using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<ActionResult<List<SimpleItemAnalysis>>> GetItemDetails() => await Mediator.Send(new Application.Analytics.List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemAnalysis>> GetItemDetail(long id) => await Mediator.Send(new Application.Analytics.Details.Query { Id = id });
    }
}

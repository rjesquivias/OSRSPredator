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
        public async Task<ActionResult<List<SimpleItemAnalysis>>> GetItemDetails(int pageSize = 100, int page = 1) => await Mediator.Send(new Application.Analytics.List.Query{ pageSize = pageSize, page = page });

        [HttpGet("[action]")]
        public async Task<ActionResult<List<SimpleItemAnalysis>>> Watchlist(int pageSize = 100, int page = 1) => await Mediator.Send(new Application.SimpleItemAnalysis.List.Query{ pageSize = pageSize, page = page });

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemAnalysis>> GetItemDetail(long id) => await Mediator.Send(new Application.Analytics.Details.Query { Id = id });

        [HttpPost("[action]")]
        public async Task<ActionResult> Watchlist([FromBody()] SimpleItemAnalysis simpleItemAnalysis) => Ok(await Mediator.Send(new Application.SimpleItemAnalysis.Post.Command{ simpleItemAnalysis = simpleItemAnalysis }));
    }
}

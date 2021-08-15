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
    public class ItemDetailsController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<ActionResult<List<DefaultItemDetails>>> GetItemDetails(int pageSize = 100, int page = 1) => await Mediator.Send(new Application.ItemDetails.List.Query{ pageSize = pageSize, page = page });

        [HttpGet("{id}")]
        public async Task<ActionResult<DefaultItemDetails>> GetItemDetail(long id) => await Mediator.Send(new Application.ItemDetails.Details.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> SyncItemDetails() => Ok(await Mediator.Send(new Application.ItemDetails.Sync.Command()));
    }
}

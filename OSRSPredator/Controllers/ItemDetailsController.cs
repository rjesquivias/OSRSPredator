using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OSRSPredator.Controllers;
using Application.Core;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemDetailsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetItemDetails([FromQuery]PagingParams param) => HandlePagedResult(await Mediator.Send(new Application.ItemDetails.List.Query{ Params = param }));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetail(long id) => HandleResult(await Mediator.Send(new Application.ItemDetails.Details.Query { Id = id }));

        [HttpPost]
        public async Task<ActionResult> SyncItemDetails() => HandleResult(await Mediator.Send(new Application.ItemDetails.Sync.Command()));
    }
}

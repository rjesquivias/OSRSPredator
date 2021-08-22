using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OSRSPredator.Controllers;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemDetailsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetItemDetails(int pageSize = 100, int page = 1) => HandleResult(await Mediator.Send(new Application.ItemDetails.List.Query{ pageSize = pageSize, page = page }));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetail(long id) => HandleResult(await Mediator.Send(new Application.ItemDetails.Details.Query { Id = id }));

        [HttpPost]
        public async Task<ActionResult> SyncItemDetails() => HandleResult(await Mediator.Send(new Application.ItemDetails.Sync.Command()));
    }
}

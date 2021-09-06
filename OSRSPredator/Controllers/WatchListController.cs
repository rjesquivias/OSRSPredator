using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain;
using OSRSPredator.Controllers;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WatchListController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetItemDetails(int pageSize = 20, int page = 1) => HandleResult(await Mediator.Send(new Application.WatchList.List.Query{ pageSize = pageSize, page = page }));

        [HttpPost]
        public async Task<IActionResult> CreateItemDetail(ItemDetails itemDetails) => HandleResult(await Mediator.Send(new Application.WatchList.Post.Command{ itemDetails = itemDetails }));

        [HttpDelete]
        public async Task<IActionResult> DeleteItemDetail(ItemDetails itemDetails) => HandleResult(await Mediator.Send(new Application.WatchList.Delete.Command{ itemDetails = itemDetails }));
    }
}
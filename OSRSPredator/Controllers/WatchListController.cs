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
    public class WatchListController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<ActionResult<List<WatchListItemDetails>>> GetItemDetails(int pageSize = 100, int page = 1) => await Mediator.Send(new Application.WatchList.List.Query{ pageSize = pageSize, page = page });

        [HttpPost]
        public async Task<ActionResult> CreateItemDetail(WatchListItemDetails itemDetails) => Ok(await Mediator.Send(new Application.WatchList.Post.Command{ itemDetails = itemDetails }));

        [HttpDelete]
        public async Task<ActionResult> DeleteItemDetail(WatchListItemDetails itemDetails) => Ok(await Mediator.Send(new Application.WatchList.Delete.Command{ itemDetails = itemDetails }));
    }
}
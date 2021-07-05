using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<ActionResult<List<ItemDetail>>> GetItemDetails() => await Mediator.Send(new Application.ItemDetails.List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDetail>> GetItemDetail(long id) => await Mediator.Send(new Application.ItemDetails.Details.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> SyncItemDetails() => Ok(await Mediator.Send(new Application.ItemDetails.Sync.Command()));
    }
}

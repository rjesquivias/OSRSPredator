using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPriceSnapshotController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<ActionResult<List<ItemPriceSnapshot>>> GetItemPriceSnapshots() => await Mediator.Send(new Application.ItemPriceSnapshots.List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPriceSnapshot>> GetItemPriceSnapshot(Guid id) => await Mediator.Send(new Application.ItemPriceSnapshots.Details.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> SyncItemPriceSnapshots() => Ok(await Mediator.Send(new Application.ItemPriceSnapshots.Sync.Command()));
    }
}

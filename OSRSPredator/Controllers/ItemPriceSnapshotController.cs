using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OSRSPredator.Controllers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPriceSnapshotController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ItemPriceSnapshot>>> GetItemPriceSnapshots() => await Mediator.Send(new Application.ItemPriceSnapshots.List.Query());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemPriceSnapshot(String id) => HandleResult(await Mediator.Send(new Application.ItemPriceSnapshots.Details.Query { Id = id }));

        [HttpPost]
        public async Task<ActionResult> SyncItemPriceSnapshots() => Ok(await Mediator.Send(new Application.ItemPriceSnapshots.Sync.Command()));
    }
}

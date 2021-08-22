using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using OSRSPredator.Controllers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemHistoricalController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ItemHistorical>>> GetItemHistoricals() => HandleResult(await Mediator.Send(new Application.ItemHistoricals.List.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemHistorical(long id) => HandleResult(await Mediator.Send(new Application.ItemHistoricals.Details.Query { Id = id }));
    }
}

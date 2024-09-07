using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Application.Feature.Pond.Commands.DeletePond;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Application.Feature.PondType.Commands.DeletePondType;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PondController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetPonds([FromQuery] string? pondId, string? pondTypeName, int pageSize = 200, int pageNumber = 1)
        {
            var ponds = await _mediator.Send(new GetAllPond());
            if (pondId != null)
            {
                ponds = ponds.Where(x => x.PondId == pondId).ToList();
            }
            if (pondTypeName != null)
            {
                ponds = ponds.Where(x => x.PondTypeName == pondTypeName).ToList();
            }
            ponds = ponds.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(ponds);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePond([FromBody] CreatePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpPut]
        public async Task<IActionResult> ActivePond([FromBody] ActivePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(id);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePond([FromQuery] string PondId)
        {
            var command = new DeletePond { PondId = PondId };
            var IdReturn = await _mediator.Send(command);
            return Ok(IdReturn);
        }
    }
}

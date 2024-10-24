using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Application.Feature.Pond.Commands.DeletePond;
using ShrimpPond.Application.Feature.Pond.Commands.HarvestPond;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime;
using ShrimpPond.Application.Feature.PondType.Commands.DeletePondType;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
using ShrimpPond.Domain.PondData;

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
        public async Task<IActionResult> GetPonds([FromQuery] string? pondId, string? pondTypeName, EPondStatus? pondStatus ,int pageSize = 200, int pageNumber = 1)
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
            if (pondStatus != null)
            {
                ponds = ponds.Where(x => x.Status == pondStatus).ToList();
            }
            ponds = ponds.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            //if (ponds.Count() ==0)
            //{
            //    throw new BadRequestException("Not found Pond");
            //}
            return Ok(ponds);
        }
        [HttpGet("GetHarvestTime")]
        public async Task<IActionResult> GetHarvestTime([FromQuery] string pondId)
        {
            var harvestTime = await _mediator.Send(new GetHarvestTime { PondId = pondId});
            return Ok(harvestTime);
        }
        [HttpPost("CreatePond")]
        public async Task<IActionResult> CreatePond([FromBody] CreatePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPost("HarvestPond")]
        public async Task<IActionResult> HarvestPond([FromBody] HarvestPond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpPut("ActivePond")]
        public async Task<IActionResult> ActivePond([FromBody] ActivePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePond([FromQuery] string PondId)
        {
            var command = new DeletePond { PondId = PondId };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}

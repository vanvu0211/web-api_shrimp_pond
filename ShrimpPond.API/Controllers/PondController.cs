using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;

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
        [HttpPost]
        public async Task<IActionResult> CreatePond([FromBody] CreatePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> ActivePond([FromBody] ActivePond e)
        {
            var id = await _mediator.Send(e);
            return Ok(id);
        }
    }
}

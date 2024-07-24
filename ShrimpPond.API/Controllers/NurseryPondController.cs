using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseryPondController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NurseryPondController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePond([FromBody] CreateNurseryPond e)
        {
            var id = await _mediator.Send(e);
            return Ok(id);

        }
    }
}

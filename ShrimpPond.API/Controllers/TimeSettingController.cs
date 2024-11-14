using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TimeSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTimeSetting([FromBody] CreateTimeSetting e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeedingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Food")]
        public async Task<IActionResult> FoodFeeding([FromBody] FoodFeeding e)
        {
            var id = await _mediator.Send(e);
            return Ok(id);
        }
        [HttpPost("Medicine")]
        public async Task<IActionResult> MedicineFeeding([FromBody] MedicineFeeding e)
        {
            var id = await _mediator.Send(e);
            return Ok(id);
        }

    }
}

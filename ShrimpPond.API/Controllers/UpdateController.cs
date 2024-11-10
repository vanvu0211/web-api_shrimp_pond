using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Update.Commands.LossShrimpUpdate;
using ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UpdateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("SizeShrimp")]
        public async Task<IActionResult> GetSizeShrimp([FromQuery] string? PondId, int pageSize = 200, int pageNumber = 1)
        {
            var sizeShrimps = await _mediator.Send(new GetSizeUpdate());
            if (PondId != null)
            {
                sizeShrimps = sizeShrimps.Where(x => x.PondId == PondId).ToList();
            }
            sizeShrimps = sizeShrimps.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(sizeShrimps);
        }
        [HttpGet("LossShrimp")]
        public async Task<IActionResult> GetLossShrimp([FromQuery] string? PondId, int pageSize = 200, int pageNumber = 1)
        {
            var LossShrimps = await _mediator.Send(new GetLossUpdate());
            if (PondId != null)
            {
                LossShrimps = LossShrimps.Where(x => x.PondId == PondId).ToList();
            }
            LossShrimps = LossShrimps.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(LossShrimps);
        }
        [HttpGet("FoodFeeding")]
        public async Task<IActionResult> GetFoodFeeding([FromQuery] string? PondId, int pageSize = 200, int pageNumber = 1)
        {
            var FoodFeedings = await _mediator.Send(new GetFoodFeeding { PondId = PondId });
            return Ok(FoodFeedings);
        }
        [HttpGet("MedicineFeeding")]
        public async Task<IActionResult> GetMedicineFeeding([FromQuery] string? PondId, int pageSize = 200, int pageNumber = 1)
        {
            var MedicineFeedings = await _mediator.Send(new GetMedicineFeeding { PondId = PondId });
            return Ok(MedicineFeedings);
        }
        [HttpPost("Food")]
        public async Task<IActionResult> FoodFeeding([FromBody] FoodFeeding e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPost("Medicine")]
        public async Task<IActionResult> MedicineFeeding([FromBody] MedicineFeeding e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }
        [HttpPost("SizeShrimp")]
        public async Task<IActionResult> SizeShrimpUpdate([FromBody] SizeShrimpUpdate sizeShrimpUpdate)
        {
            var id = await _mediator.Send(sizeShrimpUpdate);
            return Ok(sizeShrimpUpdate);
        }
        [HttpPost("LossShrimp")]
        public async Task<IActionResult> LossShrimpUpdate([FromBody] LossShrimpUpdate lossShrimpUpdate)
        {
            var id = await _mediator.Send(lossShrimpUpdate);
            return Ok(lossShrimpUpdate);
        }
    }
}

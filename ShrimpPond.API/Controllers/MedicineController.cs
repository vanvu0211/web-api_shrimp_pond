using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.Food.Commands.DeleteFood;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.Medicine.Commands.CreateMedicine;
using ShrimpPond.Application.Feature.Medicine.Commands.DeleteMedicine;
using ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicineController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicine([FromQuery] string? search, int pageSize = 200, int pageNumber = 1)
        {
            var medicines = await _mediator.Send(new GetAllMedicine());
            if (search != null)
            {
                medicines = medicines.Where(x => x.Name == search).ToList();
            }
            medicines = medicines.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(medicines);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewMedicine([FromBody] CreateMedicine createMedicine)
        {
            var id = await _mediator.Send(createMedicine);
            return Ok(createMedicine);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFood([FromQuery] string MedicineName)
        {
            var command = new DeleteMedicine { MedicineName = MedicineName };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}

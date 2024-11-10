using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Application.Feature.Farm.Command.DeleteFarm;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using ShrimpPond.Application.Feature.Food.Commands.DeleteFood;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FarmController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FarmController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetFarm([FromQuery] string? search, int pageSize = 200, int pageNumber = 1)
        {
            var farms = await _mediator.Send(new GetAllFarm());
            if (search != null)
            {
                farms = farms.Where(x => x.FarmName == search).ToList();
            }
            farms = farms.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return Ok(farms);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromBody] CreateFarm e)
        {
            var id = await _mediator.Send(e);
            return Ok(e);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFarm([FromQuery] string FarmName)
        {
            var command = new DeleteFarm { FarmName = FarmName };
            var IdReturn = await _mediator.Send(command);
            return Ok(command);
        }
    }
}

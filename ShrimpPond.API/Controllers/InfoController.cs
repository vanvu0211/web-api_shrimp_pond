using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFood([FromQuery] string pondId, int pageSize = 200, int pageNumber = 1)
        {
            var infos = await _mediator.Send(new GetInformationPond() { pondId = pondId });

            
            return Ok(infos);
        }
    }
}

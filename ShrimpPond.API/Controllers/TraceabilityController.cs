using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraceabilityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TraceabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetTraceability([FromQuery] string SeedId, int HarvestTime, int pageSize = 200, int pageNumber = 1)
        {
            var et = await _mediator.Send(new GetTraceability
            {
                SeedId = SeedId,
                HarvestTime = HarvestTime,

            });
            
            return Ok(et);

        }
    }
}

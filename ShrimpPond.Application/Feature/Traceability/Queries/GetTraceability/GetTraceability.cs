using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability
{
    public class GetTraceability: IRequest< TraceabilityDTO>
    {
        public string SeedId {  get; set; } = string.Empty;
        public int HarvestTime { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime
{
    public class GetHarvestTime: IRequest<HarvestTimeDTO>
    {
        public string PondId { get; set; } = string.Empty;
    }
}

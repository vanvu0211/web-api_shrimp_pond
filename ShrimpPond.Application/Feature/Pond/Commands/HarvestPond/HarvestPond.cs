using MediatR;
using ShrimpPond.Domain.PondData.Harves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.HarvestPond
{
    public class HarvestPond : IRequest<HarvestPond>
    {
        public EHarvest HarvestType { get; set; }
        public DateTime HarvestDate { get; set; }
        public float Amount { get; set; }
        public float Size { get; set; }
        public List<string>? Certificates { get; set; }

        public string PondId { get; set; } = string.Empty;
    }
}

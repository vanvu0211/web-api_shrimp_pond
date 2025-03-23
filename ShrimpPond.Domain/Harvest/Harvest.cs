using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.PondData.Harves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Harvest
{
    public class Harvest
    {
        public int harvestId { get; set; }
        public int harvestTime { get; set; }
        public EHarvest harvestType { get; set; }
        public string seedId { get; set; } = string.Empty;

        public List<Certificate>? certificates { get; set; }
        public float size { get; set; }
        public float amount { get; set; }
        public DateTime harvestDate { get; set; }
        public string pondId { get; set; } = string.Empty;
        public Domain.PondData.Pond? pond { get; set; }
        public int  farmId { get; set; } 
        public Domain.Farm.Farm? farm { get; set; }

    }
}

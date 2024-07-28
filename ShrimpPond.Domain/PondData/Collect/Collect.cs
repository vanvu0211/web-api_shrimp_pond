using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Collect
{
    public class Collect
    {
        public int CollectId { get; set; }
        public int CollectTime { get; set; }
        public ECollectType CollectType { get; set; }

        public List<Certificate>? Certificates { get; set; }
        public float SizeShrimpCollect { get; set; }
        public float AmountShrimpCollect { get; set; }
        public DateTime CollectDate { get; set; }

        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }
    }
}

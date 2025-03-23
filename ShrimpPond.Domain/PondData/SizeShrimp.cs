using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class SizeShrimp
    {
        public int sizeShrimpId { get; set; }
        public float sizeValue { get; set; }

        public DateTime updateDate { get; set; }
        public string pondId { get; set; } = string.Empty;
        public Pond? pond { get; set; }
    }
}

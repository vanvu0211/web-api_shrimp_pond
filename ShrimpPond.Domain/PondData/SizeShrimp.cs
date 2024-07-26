using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class SizeShrimp
    {
        public int SizeShrimpId { get; set; }
        public string Value { get; set; }
        public DateTime UpdateDate { get; set; }

        public string PondId { get; set; } = string.Empty;
    }
}

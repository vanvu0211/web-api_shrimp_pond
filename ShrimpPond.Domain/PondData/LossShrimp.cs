using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class LossShrimp
    {
        public int lossShrimpId {  get; set; }
        public float lossValue { get; set; }
        public DateTime updateDate { get; set; }
        public string pondId { get; set; } = string.Empty;
        public Pond? pond { get; set; }

    }
}

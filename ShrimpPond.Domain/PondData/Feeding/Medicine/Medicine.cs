using ShrimpPond.Domain.PondData.Collect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Feeding.Medicine
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}

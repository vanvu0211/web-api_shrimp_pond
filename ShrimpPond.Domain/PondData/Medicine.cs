using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
        public DateTime UsedDate { get; set; }

        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }
    }
}

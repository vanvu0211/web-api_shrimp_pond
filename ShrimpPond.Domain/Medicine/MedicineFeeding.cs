using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Medicine
{
    public class MedicineFeeding
    {
        public int medicineFeedingId { get; set; }
        public ICollection<MedicineForFeeding>? medicines { get; set; }
        public DateTime feedingDate { get; set; }
        public string pondId { get; set; } = string.Empty;
        public Pond? pond { get; set; }

    }
}

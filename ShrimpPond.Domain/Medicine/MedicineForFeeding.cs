using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Medicine
{
    public class MedicineForFeeding
    {

        public int medicineForFeedingId { get; set; }
        public string name { get; set; } = string.Empty;
        public float amount { get; set; }
        public int medicineFeedingId { get; set; }
        public MedicineFeeding? medicineFeeding { get; set; }
       
    }
}

using ShrimpPond.Domain.PondData.Feeding.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Feeding.Medicine
{
    public class MedicineFeeding
    {
        public int MedicineFeedingId { get; set; }
        public List<MedicineForFeeding>? Medicines { get; set; }
        public DateTime FeedingDate { get; set; }
        public string PondId { get; set; } = string.Empty;
    }
}

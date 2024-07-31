using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding
{
    public class GetMedicineFeedingDTO
    {
        public string PondId { get; set; } = string.Empty;
        public List<Medicines>? Medicines { get; set; }
        public DateTime FeedlingDate { get; set; }
    }
}

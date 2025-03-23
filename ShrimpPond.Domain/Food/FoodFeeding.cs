using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Food
{
    public class FoodFeeding
    {
        public int foodFeedingId { get; set; }
        public ICollection<FoodForFeeding>? foods { get; set; }
        public DateTime feedingDate { get; set; }
        public string pondId { get; set; } = string.Empty;
        public Pond? pond { get; set; }
    }
}

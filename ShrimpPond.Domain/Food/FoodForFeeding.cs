using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Food
{
    public class FoodForFeeding
    {
        public int foodForFeedingId { get; set; }
        public string name { get; set; } = string.Empty;
        public float amount { get; set; }
        public int foodFeedingId { get; set; }
        public FoodFeeding? foodFeeding { get; set; }

    }
}

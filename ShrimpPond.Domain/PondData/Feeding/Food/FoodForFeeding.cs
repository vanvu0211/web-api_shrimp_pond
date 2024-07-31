using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Feeding.Food
{
    public class FoodForFeeding
    {
        public int FoodForFeedingId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
        public int FoodFeedingId { get; set; }
        public FoodFeeding? FoodFeeding { get; set; }    

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class Feeding
    {
        public int FeedingId { get; set; }
        public List<FeedingFood>? Foods { get; set; }
        public DateTime FeedingDate { get; set; }
        public string PondId { get; set; } = string.Empty;
    }
}

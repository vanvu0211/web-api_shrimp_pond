using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Environments
{
    public class TemperatureValue
    {
        public int TemperatureId { get; set; }
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

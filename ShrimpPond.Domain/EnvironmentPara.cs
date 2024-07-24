using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain
{
    public class EnvironmentPara
    {
        public int EnvironmentId { get; set; }
        public PHValue? PHValue { get; set; }
        public TemperatureValue? TemperatureValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.CleanSensor
{
    public class CleanSensor
    {
        public int cleanSensorId {  get; set; }
        public DateTime cleanTime { get; set; }

        public int farmId { get; set; }
        public Domain.Farm.Farm farm { get; set; }
    }
}

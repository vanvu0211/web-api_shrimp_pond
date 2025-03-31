using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Machine
{
    public class Machine
    {
        public int machineId { get; set; }
        public string machineName { get; set; } = string.Empty;
        public List<PondId> pondIds { get; set; }
        public bool status { get; set; }
        public int farmId { get; set; }
        public Domain.Farm.Farm farm { get; set; }
    }
}

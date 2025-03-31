using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Machine
{
    public class PondId
    {
        public int id { get; set; }
        public string pondId { get; set; } = string.Empty;
        public string pondName { get; set; } = string.Empty;
        public int machineId { get; set; }
    }
}

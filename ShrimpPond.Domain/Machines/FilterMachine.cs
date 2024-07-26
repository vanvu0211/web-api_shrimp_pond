using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Machines
{
    public class FilterMachine
    {
        public string MachineId { get; set; } = string.Empty;
        public bool IsRunning { get; set; }
    }
}

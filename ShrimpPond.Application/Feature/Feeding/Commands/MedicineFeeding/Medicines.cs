using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding
{
    public class Medicines
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
    }
}

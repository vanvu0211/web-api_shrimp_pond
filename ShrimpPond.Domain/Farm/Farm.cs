using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Farm
{
    public class Farm
    {
        public int farmId { get; set; } 
        public string userName { get; set; } = string.Empty;
        public string farmName { get; set;} = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}

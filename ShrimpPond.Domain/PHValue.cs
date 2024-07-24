using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain
{
    public class PHValue
    {
        public int PhId { get; set; }   
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

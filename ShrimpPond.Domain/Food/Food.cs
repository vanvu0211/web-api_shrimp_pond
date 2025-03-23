using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Food
{
    public class Food
    {
        public int foodId { get; set; }
        public string name { get; set; } = string.Empty;

        public int farmId { get; set; }
        public Domain.Farm.Farm farm { get; set; }
    }
}

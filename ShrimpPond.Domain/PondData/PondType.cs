using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Domain.Farm;

namespace ShrimpPond.Domain.PondData
{
    public class PondType
    {
        public string pondTypeId {  get; set; } = string.Empty;
        public string pondTypeName { get; set; } = string.Empty;

        public int farmId { get; set; }
        public Domain.Farm.Farm farm { get; set; }
    }
}

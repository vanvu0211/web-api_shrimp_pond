using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain
{
    public class CommodityPond
    {
        /// <summary>
        /// Ao thương phẩm
        /// </summary>
        public string PondId { get; set; } = string.Empty;
        public float PondHeight { get; set; }
        public float PondRadius { get; set; }

        [EnumDataType(typeof(PondStatus))]
        public PondStatus Status { get; set; }

        public string SeedId { get; set; } = string.Empty;
        public string ÒriginnPondId { get; set; } = string.Empty;

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[] ShrimpCertificate { get; set; }

        public double ShrimpAmount { get; set; }
        public float ShrimpSize { get; set; }

        public DateTime StartDate { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}

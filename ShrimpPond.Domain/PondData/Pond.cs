using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class Pond
    {
        //Khởi tạo ao
        public string PondId { get; set; } = string.Empty;
        public string Deep { get; set; } = string.Empty;
        public string Diameter { get; set; } = string.Empty;
        public string PondTypeName { get; set; } = string.Empty;
        public PondType? PondType { get; set; }

        //Kích hoạt ao (Thêm SizeShrimp)
        [EnumDataType(typeof(EPondStatus))]
        public EPondStatus Status { get; set; }
        public string OriginPondId { get; set; } = string.Empty;
        public string SeedId { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public List<Certificate>? Certificates { get; set; }
        public string AmountShrimp { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }

        //Trong quá trình nuôi 
        public SizeShrimp? SizeShrimp { get; set; } 
        public LossShrimp? LossShrimp { get; set; }
        public List<Food>? Foods { get; set; }
        public List<Medicine>? Medicines { get; set; }

        //Thu hoạch
        public Collect? Collect { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Domain.PondData.Feeding.Food;
using ShrimpPond.Domain.PondData.Feeding.Medicine;
using Microsoft.VisualBasic;
using ShrimpPond.Domain.Environments;

namespace ShrimpPond.Domain.PondData
{
    public class Pond
    {
        //Khởi tạo ao
        public string PondId { get; set; } = string.Empty;
        public float Deep { get; set; } 
        public float Diameter { get; set; } 
        public string PondTypeName { get; set; } = string.Empty;
        public PondType? PondType { get; set; }

        //Kích hoạt ao (Thêm SizeShrimp)
        [EnumDataType(typeof(EPondStatus))]
        public EPondStatus Status { get; set; }
        public string OriginPondId { get; set; } = string.Empty;
        public string SeedId { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public List<Certificate>? Certificates { get; set; } 
        public float AmountShrimp { get; set; }
        public DateTime StartDate { get; set; }

        //Trong quá trình nuôi 
        public List<SizeShrimp>? SizeShrimps { get; set; } 
        public List<LossShrimp>? LossShrimps { get; set; }
        public List<FoodFeeding>? FoodFeedings { get; set; }
        public List<MedicineFeeding>? MedicineFeedings { get; set; }
        public List<EnvironmentStatus>? EnvironmentStatus { get; set; }


        //Thu hoạch
        public List<Harvest.Harvest>? Harvests { get; set; }    

    }
}

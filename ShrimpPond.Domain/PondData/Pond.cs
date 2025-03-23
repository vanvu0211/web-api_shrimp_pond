using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.Food;
using ShrimpPond.Domain.Medicine;
using ShrimpPond.Domain.Farm;

namespace ShrimpPond.Domain.PondData
{
    public class Pond
    {
        //Khởi tạo ao
        public string pondId { get; set; } = string.Empty;
        public string pondName { get; set; } = string.Empty;
        public float deep { get; set; } 
        public float diameter { get; set; }       
        public string pondTypeId { get; set; } = string.Empty;
        public PondType pondType { get; set; }

        //Kích hoạt ao (Thêm SizeShrimp)
        [EnumDataType(typeof(EPondStatus))]
        public EPondStatus status { get; set; }
        public string? originPondId { get; set; } = string.Empty;

        public string seedName { get; set; } = string.Empty;
        public string seedId { get; set; } = string.Empty;

        [Column(TypeName = "VARBINARY(MAX)")]
        public List<Certificate>? certificates { get; set; } 
        public float amountShrimp { get; set; }
        //public string UnitAmountShrimp { get; set; } = string.Empty ;
        public DateTime startDate { get; set; }

        //Trong quá trình nuôi 
        public ICollection<SizeShrimp>? sizeShrimps { get; set; } 
        public ICollection<LossShrimp>? lossShrimps { get; set; }
        public ICollection<FoodFeeding>? foodFeedings { get; set; }
        public ICollection<MedicineFeeding>? medicineFeedings { get; set; }
        public ICollection<EnvironmentStatus>? environmentStatus { get; set; }


        //Thu hoạch
        public ICollection<Harvest.Harvest>? harvests { get; set; }
        public int farmId { get; set; }
    }
}

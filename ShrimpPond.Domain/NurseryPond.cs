using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShrimpPond.Domain
{
    public class NurseryPond
    {   
       //Khởi tạo ao
        public string PondId { get; set; } = string.Empty;
        public string PondHeight { get; set; } = string.Empty;
        public string PondRadius { get; set; } = string.Empty;

        [EnumDataType(typeof(PondStatus))]
        public PondStatus Status { get; set; }
        //Kích hoạt
        public string SeedId { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? ShrimpCertificate { get; set; }
        public string ShrimpAmount {  get; set; } = string.Empty;
        public string ShrimpSize {  get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        //Trong quá trình nuôi
        public List<Food>? Foods { get; set; } 
        public List<Medicine>? Medicines { get; set; } 
        public EnvironmentPara? Environments { get; set; }
    }
}

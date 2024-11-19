using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetAllPond
{
    public class PondDTO
    {
        public string PondId { get; set; } = string.Empty;
        public float Deep { get; set; }
        public float Diameter { get; set; }
        public string PondTypeName { get; set; } = string.Empty;

        //Kích hoạt ao (Thêm SizeShrimp)
        [EnumDataType(typeof(EPondStatus))]
        public EPondStatus Status { get; set; }
        public string? OriginPondId { get; set; } = string.Empty;
        public string SeedId { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public float AmountShrimp { get; set; }
        public DateTime StartDate { get; set; }

    }
}

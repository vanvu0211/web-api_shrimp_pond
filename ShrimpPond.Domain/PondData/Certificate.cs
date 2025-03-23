using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class Certificate
    {
        public int certificateId { get; set; }
        public string certificateName { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? fileData { get; set; }

        public string pondId { get; set; } = string.Empty;
        public Pond? pond { get; set; }

    }
}

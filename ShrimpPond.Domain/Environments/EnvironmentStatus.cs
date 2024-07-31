using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Environments
{
    public class EnvironmentStatus
    {
        public int EnvironmentStatusId {  get; set; }
        public string Name { get; set; } = string.Empty;
        public float Value { get; set; }
        public DateTime Timestamp   { get; set; }

        public string PonId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }
    }
}

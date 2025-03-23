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
        public int environmentStatusId {  get; set; }
        public string name { get; set; } = string.Empty;
        public string value { get; set; } = string.Empty;
        public DateTime? timestamp   { get; set; }

        public string pondId { get; set; } = string.Empty;
        public Domain.PondData.Pond pond { get; set; }

    }
}

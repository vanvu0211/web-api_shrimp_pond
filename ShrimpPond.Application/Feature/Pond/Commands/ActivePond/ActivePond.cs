using MediatR;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond
{
    public class ActivePond: IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public string SeedId { get; set; } = string.Empty;
        public List<string>? Certificates { get; set; }
        public string SizeShrimp { get; set; } = string.Empty;
        public string AmountShrimp { get; set; } = string.Empty;
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreatePond:IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public string PondTypeName { get; set; } = string.Empty ;
        public string Deep { get; set; } = string.Empty;
        public string Diameter { get; set; } = string.Empty;
    }
}

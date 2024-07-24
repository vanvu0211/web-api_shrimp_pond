using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreateNurseryPond:IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public string PondHeight { get; set; } = string.Empty;
        public string PondRadius { get; set; } = string.Empty;
    }
}

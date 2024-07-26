using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.CreatePondType
{
    public class CreatePondType:IRequest<string>
    {
        public string PondTypeId { get; set; } = string.Empty;
        public string PondTypeName {  get; set; } = string.Empty;
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate
{
    public class SizeShrimpUpdate: IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public float SizeValue { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

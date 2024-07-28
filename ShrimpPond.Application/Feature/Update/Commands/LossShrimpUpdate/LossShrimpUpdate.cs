using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.LossShrimpUpdate
{
    public class LossShrimpUpdate: IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public float LossValue { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

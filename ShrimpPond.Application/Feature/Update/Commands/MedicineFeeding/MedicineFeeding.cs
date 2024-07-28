using MediatR;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding
{
    public class MedicineFeeding : IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public List<Medicines>? Medicines { get; set; }
        public DateTime FeedlingDate { get; set; }
    }
}

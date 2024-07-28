using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.Application.Feature.Food.Commands.Feeding
{
    public class Feeding: IRequest<string>
    {
        public string PondId { get; set; } = string.Empty;
        public List<FoodFeeding>? Foods { get; set; }
        public DateTime FeedlingDate { get; set; }
    }
}

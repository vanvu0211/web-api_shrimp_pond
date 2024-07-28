using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Queries.GetAllFood
{
    public record GetAllFood : IRequest<List<GetAllFoodDTO>>
    {
    }
}

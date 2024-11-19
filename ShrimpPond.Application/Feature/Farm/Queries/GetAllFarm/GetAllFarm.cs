using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm
{
    public record GetAllFarm: IRequest<List<FarmDTO>>
    {
    }
}

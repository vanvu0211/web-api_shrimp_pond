using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.Collect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface IPondRepository: IRepositoryBaseAsync<Pond,string>
    {

    }
}

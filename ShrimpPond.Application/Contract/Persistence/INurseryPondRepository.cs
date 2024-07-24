using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface INurseryPondRepository: IRepositoryBaseAsync<NurseryPond,string>
    {

    }
}

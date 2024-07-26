using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    public class TemperatureRepository : RepositoryBase<TemperatureValue, int>, ITemperatureValueRepository
    {
        public TemperatureRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
}


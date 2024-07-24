using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence.Genenric
{
    public interface IUnitOfWork
    {
        INurseryPondRepository nurseryPondRepository {  get; }

        Task<int> CommitAsync();
        Task SaveChangeAsync();
    }
}

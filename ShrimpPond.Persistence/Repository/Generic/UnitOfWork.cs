using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository.Generic
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ShrimpPondDbContext _context;

        public UnitOfWork(ShrimpPondDbContext context)
        {
            _context = context;
            nurseryPondRepository = new NurseryPondRepository(context);
            
        }
        public INurseryPondRepository nurseryPondRepository { get; private set; }
      

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

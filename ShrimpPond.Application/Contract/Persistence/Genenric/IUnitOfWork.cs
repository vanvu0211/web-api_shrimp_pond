using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence.Genenric
{
    public interface IUnitOfWork
    {
        IPondRepository pondRepository {  get; }
        ICertificateRepository certificateRepository {  get; }
        ISizeShrimpRepository sizeShrimpRepository {  get; }
        IPondTypeRepository pondTypeRepository {  get; }
        IFoodFeedingRepository foodFeedingRepository {  get; }
        IFoodForFeedingRepository foodForFeedingRepository {  get; }
        IFoodRepository foodRepository {  get; }
        IMedicineFeedingRepository medicineFeedingRepository { get; }
        IMedicineForFeedingRepository medicineForFeedingRepository { get;}
        IMedicineRepository medicineRepository { get; }
        ILossShrimpRepository lossShrimpRepository { get; }

        Task<int> CommitAsync();
        Task SaveChangeAsync();
    }
}

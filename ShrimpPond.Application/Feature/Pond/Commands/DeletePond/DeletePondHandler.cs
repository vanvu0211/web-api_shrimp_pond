using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Commands.DeletePondType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.DeletePond
{
    public class DeletePondHandler: IRequestHandler<DeletePond,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeletePond request, CancellationToken cancellationToken)
        {

            //validate

            var deletePond = await _unitOfWork.pondRepository.GetByIdAsync(request.PondId);

            if (deletePond == null)
            {
                throw new BadRequestException("Not found PondType");
            }
             var feedings = _unitOfWork.feedingRepository.FindByCondition(x=> x.PondId == request.PondId);
            _unitOfWork.feedingRepository.RemoveRange(feedings);

            foreach (var feeding in feedings)
            {
                var feedingFoods = _unitOfWork.feedingFoodRepository.FindByCondition(x => x.FeedingId == feeding.FeedingId);
                _unitOfWork.feedingFoodRepository.RemoveRange(feedingFoods);
            }


            _unitOfWork.pondRepository.Remove(deletePond);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondId;
        }

    }
}

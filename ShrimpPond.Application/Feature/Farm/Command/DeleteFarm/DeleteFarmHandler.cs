using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Pond.Commands.DeletePond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.DeleteFarm
{
    public class DeleteFarmHandler: IRequestHandler<DeleteFarm,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFarmHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteFarm request, CancellationToken cancellationToken)
        {

            //validate
            var deleteFarm =  _unitOfWork.farmRepository.FindByCondition(f=>f.FarmName == request.FarmName).FirstOrDefault();
            if (deleteFarm == null)
            {
                throw new BadRequestException("Not found Farm");
            }
            //Xóa danh sách cho ăn 
            var foodfeedings = _unitOfWork.pondTypeRepository.FindByCondition(x => x.FarmName == request.FarmName).FirstOrDefault();
            if (foodfeedings != null)
            {
                throw new BadRequestException("PondType is still exits in Farm");
            }
           
            _unitOfWork.farmRepository.Remove(deleteFarm);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.FarmName;
        }
    }
}

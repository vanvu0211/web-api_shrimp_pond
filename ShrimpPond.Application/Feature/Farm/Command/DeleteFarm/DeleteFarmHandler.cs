﻿using MediatR;
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
    public class DeleteFarmHandler: IRequestHandler<DeleteFarm,int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFarmHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteFarm request, CancellationToken cancellationToken)
        {

            //validate
            var deleteFarm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (deleteFarm == null)
            {
                throw new BadRequestException("Not found Farm");
            }

            var pondType = _unitOfWork.pondTypeRepository.FindByCondition(x => x.farmId == deleteFarm.farmId).FirstOrDefault();
            if (pondType != null)
            {
                throw new BadRequestException("PondType is still exits in Farm");
            }
            


            _unitOfWork.farmRepository.Remove(deleteFarm);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return deleteFarm.farmId;
        }
    }
}

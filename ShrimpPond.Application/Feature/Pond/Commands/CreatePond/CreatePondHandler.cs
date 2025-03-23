﻿using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.Application.Feature.Pond.Commands.CreatePond
{
    public class CreatePondHandler: IRequestHandler<NurseryPond.Commands.CreatePond.CreatePond, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(NurseryPond.Commands.CreatePond.CreatePond request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreatePondValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
           
            var condition1 =  _unitOfWork.pondRepository.FindAll().SingleOrDefault(p=>p.pondName == request.pondName && p.pondTypeId == request.pondTypeId );

            if (condition1 != null)
            {
                throw new BadRequestException("PondId already exist");
            }

            var pondType = await _unitOfWork.pondTypeRepository.GetByIdAsync(request.pondTypeId);

            if (pondType == null)
            {
                throw new BadRequestException("Not found PondType");
            }

       

            var nurseryPond = new Domain.PondData.Pond()
            {
                pondId = request.pondId,
                pondName = request.pondName,
                deep = request.deep,
                diameter = request.diameter,
                pondTypeId = request.pondTypeId,
                startDate = DateTime.Now,
                status = EPondStatus.InActive,
                farmId = pondType.farmId
            };


            _unitOfWork.pondRepository.Add(nurseryPond);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.pondId;
        }

    }
}

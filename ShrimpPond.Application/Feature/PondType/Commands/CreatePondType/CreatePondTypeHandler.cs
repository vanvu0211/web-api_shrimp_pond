﻿using FluentValidation;
using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.CreatePondType
{
    public class CreatePondTypeHandler : IRequestHandler<CreatePondType, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePondTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreatePondType request, CancellationToken cancellationToken)
        {

            //validate
            var validator = new CreatePondTypeValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }

            var pondType = _unitOfWork.pondTypeRepository.FindByCondition(x =>  x.pondTypeName == request.pondTypeName && x.farmId == farm.farmId).FirstOrDefault();
            if (pondType != null)
            {
                throw new BadRequestException("pondTypeName duplicate");
            }
            var createPondType = new Domain.PondData.PondType()
            {
                pondTypeId = request.pondTypeId,
                pondTypeName = request.pondTypeName,
                farmId = farm.farmId,
            };

           _unitOfWork.pondTypeRepository.Add(createPondType);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.pondTypeName;
        }
    }
}

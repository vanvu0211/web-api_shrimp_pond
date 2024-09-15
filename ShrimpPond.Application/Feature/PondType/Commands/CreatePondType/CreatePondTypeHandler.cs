using FluentValidation;
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
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }

            var farm = _unitOfWork.farmRepository.FindByCondition(x=>x.FarmName == request.FarmName).FirstOrDefault();
            if(farm == null)
            {
                throw new BadRequestException("Not Found Farm");
            }

            var createPondType = new Domain.PondData.PondType()
            {
                PondTypeId = request.PondTypeId,
                PondTypeName = request.PondTypeName,
                FarmName = request.FarmName,
            };

           _unitOfWork.pondTypeRepository.Add(createPondType);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondTypeName;
        }
    }
}

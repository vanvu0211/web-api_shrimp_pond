using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreataPondHandler: IRequestHandler<CreatePond, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreataPondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreatePond request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreatePondValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert

            var Condition1 =  _unitOfWork.pondRepository.FindAll().SingleOrDefault(p=>p.PondId == request.PondId);

            if (Condition1 != null)
            {
                throw new BadRequestException("PondId already exist");
            }

            var Condition2 = _unitOfWork.pondTypeRepository.FindAll().SingleOrDefault(p => p.PondTypeName == request.PondTypeName);

            if (Condition2 == null)
            {
                throw new BadRequestException("Not found PondType");
            }

            var nurseryPond = new Domain.PondData.Pond()
            {
                PondId = request.PondId,
                Deep = request.Deep,
                Diameter = request.Diameter,
                PondTypeName = request.PondTypeName,
                StartDate = DateTime.Now,
                Status = EPondStatus.InActive,
            };


            _unitOfWork.pondRepository.Add(nurseryPond);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondId;
        }

    }
}

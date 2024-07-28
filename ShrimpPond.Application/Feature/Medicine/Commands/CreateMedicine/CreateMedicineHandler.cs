using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.CreateMedicine
{
    public class CreateMedicineHandler : IRequestHandler<CreateMedicine, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateMedicine request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateMedicineValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var condition = _unitOfWork.medicineRepository.FindAll().Where(f => f.Name == request.Name).ToList();
            if (condition.Count() != 0)
            {
                throw new BadRequestException("Medicine already exist", validatorResult);
            }

            var newmedicine = new Domain.PondData.Feeding.Medicine.Medicine()
            {
                Name = request.Name,
                Type = request.Type,
            };

            _unitOfWork.medicineRepository.Add(newmedicine);
            await _unitOfWork.SaveChangeAsync();
            return request.Name;
        }
    }
}

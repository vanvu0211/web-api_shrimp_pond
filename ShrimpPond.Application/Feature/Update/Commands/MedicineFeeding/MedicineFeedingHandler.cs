using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Domain.Medicine;

namespace ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding
{
    internal class MedicineFeedingHandler : IRequestHandler<MedicineFeeding, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public MedicineFeedingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(MedicineFeeding request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new MedicineFeedingValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var pond = _unitOfWork.pondRepository.FindByCondition(x => x.pondId == request.pondId && x.status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.pondId} chưa kích hoạt", validatorResult);
            }

            List<MedicineForFeeding> medicineFeedings = new List<MedicineForFeeding>();

            if (request.medicines != null)
            {
                foreach (var medicine in request.medicines)
                {
                    var medicinefeeding = new MedicineForFeeding()
                    {
                        name = medicine.name,
                        amount = medicine.amount
                    };
                    medicineFeedings.Add(medicinefeeding);
                }
            }

            var feeding = new Domain.Medicine.MedicineFeeding()
            {
                pondId = request.pondId,
                feedingDate = request.feedingDate,
                medicines = medicineFeedings
            };

            _unitOfWork.medicineFeedingRepository.Add(feeding);
            await _unitOfWork.SaveChangeAsync();
            return request.pondId;
        }
    }
}

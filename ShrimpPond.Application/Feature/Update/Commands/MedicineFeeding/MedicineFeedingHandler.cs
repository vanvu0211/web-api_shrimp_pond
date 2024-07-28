using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Domain.PondData.Feeding.Food;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Domain.PondData.Feeding.Medicine;

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

            var pond = _unitOfWork.pondRepository.FindAll().Where(x => x.PondId == request.PondId).Where(x => x.Status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.PondId} chưa kích hoạt", validatorResult);
            }

            List<MedicineForFeeding> medicineFeedings = new List<MedicineForFeeding>();

            if (request.Medicines != null)
            {
                foreach (var medicine in request.Medicines)
                {
                    var medicinefeeding = new MedicineForFeeding()
                    {
                        Name = medicine.Name,
                        Type = medicine.Type,
                        Amount = medicine.Amount
                    };
                    medicineFeedings.Add(medicinefeeding);
                }
            }

            var feeding = new Domain.PondData.Feeding.Medicine.MedicineFeeding()
            {
                PondId = request.PondId,
                FeedingDate = request.FeedlingDate,
                Medicines = medicineFeedings
            };

            _unitOfWork.medicineFeedingRepository.Add(feeding);
            await _unitOfWork.SaveChangeAsync();
            return request.PondId;
        }
    }
}

using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreataNurseryPondHandler: IRequestHandler<CreateNurseryPond, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreataNurseryPondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateNurseryPond request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateNurseryPondValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert
            var nurseryPond = new Domain.NurseryPond()
            {
                PondId = request.PondId,
                PondHeight = request.PondHeight,
                PondRadius = request.PondRadius,
                Environments = null,
                Foods = null,
                Medicines = null,
                ShrimpAmount = "",
                SeedId ="",
                ShrimpCertificate = null,
                ShrimpSize = "",
                StartDate =DateTime.Now,
                Status = Domain.PondStatus.Inactive,
            };
            _unitOfWork.nurseryPondRepository.Add(nurseryPond);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondId;
        }

    }
}

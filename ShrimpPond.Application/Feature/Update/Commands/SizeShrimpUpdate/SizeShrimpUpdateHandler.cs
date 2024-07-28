using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate
{
    public class SizeShrimpUpdateHandle : IRequestHandler<SizeShrimpUpdate, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SizeShrimpUpdateHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(SizeShrimpUpdate request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new SizeShrimpUpdateValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //handler

            var pond = _unitOfWork.pondRepository.FindAll().Where(x => x.PondId == request.PondId).Where(x => x.Status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.PondId} chưa kích hoạt", validatorResult);
            }

            var updatesize = new SizeShrimp()
            {
                PondId = request.PondId,
                SizeValue = request.SizeValue,
                UpdateDate = request.UpdateDate,
            }
;           _unitOfWork.sizeShrimpRepository.Add(updatesize);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondId;
        }
    }
}

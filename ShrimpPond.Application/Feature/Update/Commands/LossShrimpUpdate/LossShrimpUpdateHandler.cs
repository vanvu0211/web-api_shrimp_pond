using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.LossShrimpUpdate
{
    public class LossShrimpUpdateHandler: IRequestHandler<LossShrimpUpdate,string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LossShrimpUpdateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(LossShrimpUpdate request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new LossShrimpUpdateValidation();
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

            var updateloss = new LossShrimp()
            {
                PondId = request.PondId,
                LossValue = request.LossValue,
                UpdateDate = request.UpdateDate,
            };           
            _unitOfWork.lossShrimpRepository.Add(updateloss);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.PondId;
        }
    }
}

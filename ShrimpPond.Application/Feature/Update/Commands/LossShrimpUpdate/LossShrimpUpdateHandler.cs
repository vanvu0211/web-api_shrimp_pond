﻿using MediatR;
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

            var pond = _unitOfWork.pondRepository.FindByCondition(x => x.pondId == request.pondId && x.status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.pondId} chưa kích hoạt", validatorResult);
            }

            var updateloss = new LossShrimp()
            {
                pondId = request.pondId,
                lossValue = request.lossValue,
                updateDate = request.updateDate,
            };           
            _unitOfWork.lossShrimpRepository.Add(updateloss);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.pondId;
        }
    }
}

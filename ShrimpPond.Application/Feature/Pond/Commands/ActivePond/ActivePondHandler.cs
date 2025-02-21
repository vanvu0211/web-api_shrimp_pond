﻿using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.Application.Feature.Pond.Commands.ActivePond
{
    public class ActivePondHandler: IRequestHandler<NurseryPond.Commands.ActiveNurseryPond.ActivePond, string>
    
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActivePondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(NurseryPond.Commands.ActiveNurseryPond.ActivePond request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new ActivePondValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle
            
            var activePond = await _unitOfWork.pondRepository.GetByIdAsync(request.PondId);
            if(activePond == null)
            {
                throw new NotFoundException(nameof(NurseryPond), request.PondId);
            }

            var sizeShrimp = new Domain.PondData.SizeShrimp()
            {
                PondId = request.PondId,
                SizeValue = request.SizeShrimp,
                UpdateDate = DateTime.Now,
            };

            _unitOfWork.sizeShrimpRepository.Add(sizeShrimp);
            //Active
            activePond.StartDate = DateTime.Now;
            activePond.Status = EPondStatus.Active;
            activePond.AmountShrimp = request.AmountShrimp;
            //activePond.UnitAmountShrimp = request.UnitAmountShrimp;
            activePond.OriginPondId = request.OriginPondId;
            if (request.Certificates != null)
            {
                foreach (var pic in request.Certificates)
                {
                    var certificate= new  Domain.PondData.Certificate()
                    {
                        CertificateName = "Giấy xét nghiệm tôm thương phẩm",
                        PondId = request.PondId,
                        FileData = Convert.FromBase64String(pic),
                    };
                    _unitOfWork.certificateRepository.Add(certificate);
                }
            }
            activePond.SeedId = request.SeedId;
            activePond.SeedName = request.SeedName;

            _unitOfWork.pondRepository.Update(activePond);
            await _unitOfWork.SaveChangeAsync();
            return  request.PondId;
        }
    }
}

﻿using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Domain.PondData.Harvest;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.HarvestPond
{
    public class HarvestPondHandler : IRequestHandler<HarvestPond, HarvestPond>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HarvestPondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<HarvestPond> Handle(HarvestPond request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new HarvestPondValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert
            var pond = _unitOfWork.pondRepository.FindByCondition(p=>p.PondId == request.PondId).FirstOrDefault(p => p.Status == EPondStatus.Active);

            if (pond == null)
            {
                throw new BadRequestException($"Ao {request.PondId} chưa kích hoạt", validatorResult);
            }

            var timeHarvest = _unitOfWork.harvestRepository.FindAll().Count(p => p.PondId == request.PondId) + 1;// Tự động tăng số lần thu hoạch

           

            //Thu toàn bộ
            if (request.HarvestType == Domain.PondData.Harves.EHarvest.TotalCollect)
            {
                //Cho ao về trạng thái chờ
                var inActivePond = await _unitOfWork.pondRepository.GetByIdAsync(request.PondId);
                if (inActivePond != null)
                {
                    inActivePond.Status = EPondStatus.InActive;
                    _unitOfWork.pondRepository.Update(inActivePond);
                }

            }
            //Thu tỉa
            var  harvest = new Harvest()
            {
                HarvestDate = request.HarvestDate,
                Amount = request.Amount,
                //Unit = request.Unit,
                Size = request.Size,
                PondId = request.PondId,
                HarvestType = request.HarvestType,
                HarvestTime = timeHarvest,
                SeedId = pond.SeedId
            };
            _unitOfWork.harvestRepository.Add(harvest);

            var certificates = new List<Certificate>();
            if (certificates == null) throw new ArgumentNullException(nameof(certificates));
            if (request.Certificates != null)
            {
                foreach (var cer in request.Certificates)
                {
                    try
                    {
                        var certificate = new Certificate()
                        {
                            CertificateName = $"Giấy xét nghiệm kháng sinh lần thu {timeHarvest}" ,
                            FileData = Convert.FromBase64String(cer),
                            PondId = request.PondId
                        };
                        certificates.Add(certificate);
                        _unitOfWork.certificateRepository.Add(certificate);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            await _unitOfWork.SaveChangeAsync();
            //return 
            return request;
        }
    }
}

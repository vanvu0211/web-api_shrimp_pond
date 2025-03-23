using MediatR;
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
            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var pond = _unitOfWork.pondRepository.FindByCondition(p=>p.pondId == request.pondId).FirstOrDefault(p => p.status == EPondStatus.Active);

            if (pond == null)
            {
                throw new BadRequestException($"Ao {request.pondId} chưa kích hoạt", validatorResult);
            }

            var timeHarvest = _unitOfWork.harvestRepository.FindByCondition(x=>x.pondId == request.pondId).Count()+1;// Tự động tăng số lần thu hoạch

           

            //Thu toàn bộ
            if (request.harvestType == Domain.PondData.Harves.EHarvest.TotalCollect)
            {
                //Cho ao về trạng thái chờ
                var inActivePond =  _unitOfWork.pondRepository.FindByCondition(x => x.pondId == request.pondId).FirstOrDefault();
                if (inActivePond != null)
                {
                    inActivePond.status = EPondStatus.InActive;
                    _unitOfWork.pondRepository.Update(inActivePond);
                }

            }
            //Thu tỉa
            var  harvest = new Harvest()
            {
                harvestDate = request.harvestDate,
                amount = request.amount,
                size = request.size,
                pondId = request.pondId,
                harvestType = request.harvestType,
                harvestTime = timeHarvest,
                seedId = pond.seedId,
                farmId = farm.farmId
               
            };
            _unitOfWork.harvestRepository.Add(harvest);

            var certificates = new List<Certificate>();
            if (certificates == null) throw new ArgumentNullException(nameof(certificates));
            if (request.certificates != null)
            {
                foreach (var cer in request.certificates)
                {
                    try
                    {
                        var certificate = new Certificate()
                        {
                            certificateName = $"Giấy xét nghiệm kháng sinh lần thứ {timeHarvest}" ,
                            fileData = Convert.FromBase64String(cer),
                            pondId = request.pondId,
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

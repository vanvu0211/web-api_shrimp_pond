using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability
{
    public class GetTraceabilityHandler: IRequestHandler<GetTraceability,TraceabilityDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetTraceability> _logger;

        public GetTraceabilityHandler(IUnitOfWork unitOfWork, IAppLogger<GetTraceability> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<TraceabilityDTO> Handle(GetTraceability request, CancellationToken cancellationToken)
        {
            //query
            TraceabilityDTO traceabilitie = new();

            var harvests = _unitOfWork.harvestRepository.FindByCondition(x=>x.SeedId == request.SeedId).Where(x=>x.HarvestTime == request.HarvestTime).ToList();
            foreach(var harvest in harvests)
            {
                var pond = _unitOfWork.pondRepository.FindByCondition(p => p.PondId == harvest.PondId).FirstOrDefault();
                if(pond == null)
                {
                    throw new BadRequestException("Not found Pond");
                }
                traceabilitie.DaysOfRearing =(harvest.HarvestDate - pond.StartDate).Days;
                //tổng số lượng tôm các ao
                traceabilitie.Amount += harvest.Amount;
                traceabilitie.Size = harvest.Size;
                traceabilitie.HarvestPondId = traceabilitie.HarvestPondId + "-" + harvest.PondId;
                var certificates =  _unitOfWork.certificateRepository.FindByCondition(x=>x.PondId == harvest.PondId).Where(x=>x.CertificateName== $"Giấy xét nghiệm kháng sinh lần thu {request.HarvestTime}").ToList();
                
                foreach(var certificate in certificates)
                {
                    traceabilitie.Certificates.Add(certificate.FileData);
                }
                
                var pondType = _unitOfWork.pondTypeRepository.FindByCondition(x=>x.PondTypeName==pond.PondTypeName).FirstOrDefault();
                if(pondType == null)
                {
                    throw new BadRequestException("Not found PondType");
                }

                traceabilitie.FarmName = pondType.FarmName;

                var farm = _unitOfWork.farmRepository.FindByCondition(x=>x.FarmName == pondType.FarmName).FirstOrDefault();
                if (farm == null)
                {
                    throw new BadRequestException("Not found Farm");
                }
                traceabilitie.Address = farm.Address;
            }
           
            traceabilitie.SeedId = request.SeedId;
            traceabilitie.HarvestTime = request.HarvestTime;
            



            //if (pond == null)
            //{
            //    throw new BadRequestException("Not Found pond");
            //}

            //TraceabilityDTO traceabilityDTO = new TraceabilityDTO();
            //traceabilityDTO.SeedId = request.SeedId;
            //traceabilityDTO.HarvestPondId = pond.OriginPondId + "-" + pond.PondId;





            //logging
            _logger.LogInformation("Get location successfully");
            // convert
            
            //return
            return traceabilitie;
        }
    }
}

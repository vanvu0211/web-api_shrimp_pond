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
            TraceabilityDTO Traceabilitie = new();

            var harvestPonds = _unitOfWork.harvestRepository.FindByCondition(x=>x.SeedId == request.SeedId).Where(x=>x.HarvestTime == request.HarvestTime).ToList();

            if(harvestPonds.Count == 0)
            {
                throw new BadRequestException("Not found");
            }

            List<string> HarvestPondIds = new List<string>();
            List<string> HarvestSizes = new List<string>();
            DateTime EndDate = DateTime.Now;
            float Size = 0;

            foreach (var harvestPond in harvestPonds)
            {
                var pond = _unitOfWork.pondRepository.FindByCondition(p => p.PondId == harvestPond.PondId).FirstOrDefault();
                if(pond == null)
                {
                    throw new BadRequestException("Not found Pond");
                }
                //Nếu có ao gốc
                if(pond.OriginPondId != null)
                {
                    var _originPond = _unitOfWork.pondRepository.FindByCondition(p=>p.PondId==pond.OriginPondId).FirstOrDefault();
                    if(_originPond != null)
                    {
                        EndDate = _originPond.StartDate;
                    } 
                } 
                EndDate = pond.StartDate;
                Traceabilitie.DaysOfRearing =(harvestPond.HarvestDate - EndDate).Days;

                //Lấy trung bình size tôm
                HarvestSizes.Add(harvestPond.PondId + "-" + harvestPond.Size.ToString());
                Size += harvestPond.Size;

                //tổng số lượng tôm các ao
                Traceabilitie.TotalAmount += harvestPond.Amount;

                //Danh sách ao thu hoạch
                HarvestPondIds.Add(pond.OriginPondId+ "-" + harvestPond.PondId);

                //Danh sách giấy xét nghiệm
                var certificates =  _unitOfWork.certificateRepository.FindByCondition(x=>x.PondId == harvestPond.PondId).Where(x=>x.CertificateName== $"Giấy xét nghiệm kháng sinh lần thu {request.HarvestTime}").ToList();

                foreach (var certificate in certificates)
                {
                    Traceabilitie.Certificates.Add(certificate.FileData);
                }
                
                var pondType =  _unitOfWork.pondTypeRepository.FindByCondition(x=>x.PondTypeName==pond.PondTypeName).FirstOrDefault();
                if(pondType == null)
                {
                    throw new BadRequestException("Not found PondType");
                }

                Traceabilitie.FarmName = pondType.FarmName;

                var farm = _unitOfWork.farmRepository.FindByCondition(x=>x.FarmName == pondType.FarmName).FirstOrDefault();
                if (farm == null)
                {
                    throw new BadRequestException("Not found Farm");
                }
                Traceabilitie.Address = farm.Address;
            }
            Traceabilitie.SeedId = request.SeedId;
            Traceabilitie.HarvestTime = request.HarvestTime;
            
            //Tạo mã ao thu hoạch theo đúng format
            foreach(var HarvestPondId in HarvestPondIds)
            {
                Traceabilitie.HarvestPondId += HarvestPondId + ";";
            }

            if (Traceabilitie.HarvestPondId.EndsWith(";"))
            {
                Traceabilitie.HarvestPondId = Traceabilitie.HarvestPondId.Remove(Traceabilitie.HarvestPondId.Length - 1);
            }

            //Tạo size theo format

            foreach (var HarvestSize in HarvestSizes)
            {
                Traceabilitie.Size += HarvestSize + ";";
            }

            if (Traceabilitie.Size.EndsWith(";"))
            {
                Traceabilitie.Size = Traceabilitie.Size.Remove(Traceabilitie.Size.Length - 1);
            }

            Traceabilitie.Size = $"[{(Size / harvestPonds.Count())}]" +"["+ Traceabilitie.Size+"]";

            _logger.LogInformation("Get traceabilitie successfully");

            
            //return
            return Traceabilitie;
        }
    }
}

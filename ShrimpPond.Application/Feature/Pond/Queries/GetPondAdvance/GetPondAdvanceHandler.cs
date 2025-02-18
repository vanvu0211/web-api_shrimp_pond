using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetPondAdvance
{
    public class GetPondAdvanceHandler:  IRequestHandler<GetPondAdvance, List<GetPondAdvanceDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetPondAdvance> _logger;

        public GetPondAdvanceHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetPondAdvance> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<GetPondAdvanceDTO>> Handle(GetPondAdvance request, CancellationToken cancellationToken)
        {
            //query
            var data = new List<GetPondAdvanceDTO>();

            var farm = _unitOfWork.farmRepository.FindByCondition(x=>x.FarmName == request.farmName).FirstOrDefault();
            if (farm == null)
            {
                throw new BadRequestException("Not Found Farm");
            }

            var pondTypes = _unitOfWork.pondTypeRepository.FindByCondition(x=>x.FarmName == farm.FarmName).ToList();

            foreach(var pondType in pondTypes)
            {
                if (pondType == null)
                {
                    throw new BadRequestException("Not Found PondType");
                }
                var ponds = _unitOfWork.pondRepository.FindByCondition(x => x.PondTypeName == pondType.PondTypeName).ToList();

                foreach (var pond in ponds)
                {
                    var dt = new GetPondAdvanceDTO()
                    {
                        PondId = pond.PondId,
                        PondTypeName = pond.PondTypeName,
                        OriginPondId = pond.OriginPondId,
                        SeedId = pond.SeedId,
                        AmountShrimp = pond.AmountShrimp,
                        Deep = pond.Deep,
                        Diameter = pond.Diameter,
                        Status = pond.Status
                    };

                    if (pond.OriginPondId == "")
                    {

                        dt.StartDate = pond.StartDate;
                    }
                    else
                    {
                        var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.OriginPondId);
                        if (originPond == null)
                        {
                            throw new BadRequestException("Not Found Pond");
                        }
                        dt.StartDate = originPond.StartDate;
                    }



                    data.Add(dt);
                }
            }

            
            //logging
            _logger.LogInformation("Get pond successfully");
            // convert
            //var data = _mapper.Map<List<PondDTO>>(pondTypes);
            //return
            return data;
        }
    }
}

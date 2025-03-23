using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetAllPond
{
    public class GetAllPondHandler: IRequestHandler<GetAllPond,List<PondDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetAllPond> _logger;

        public GetAllPondHandler(IMapper mapper,IUnitOfWork unitOfWork,  IAppLogger<GetAllPond> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<PondDTO>> Handle(GetAllPond request, CancellationToken cancellationToken)
            {
            //query
            var data = new List<PondDTO>();
            var pondTypes =  _unitOfWork.pondTypeRepository.FindByCondition(x=>x.farmId == request.farmId).ToList();
            if (pondTypes == null)
            {
                throw new BadRequestException("Not Found pondTypes");
            }

            foreach(var pondType in pondTypes)
            {

                var ponds = _unitOfWork.pondRepository.FindByCondition(x => x.pondTypeId == pondType.pondTypeId).ToList();

            foreach (var pond in ponds)
                {
                   
                    var dt = new PondDTO()
                    {
                        pondId = pond.pondId,
                        pondName = pond.pondName,
                        pondTypeName = pondType.pondTypeName,
                        pondTypeId = pondType.pondTypeId,
                        originPondId = pond.originPondId,
                        seedId = pond.seedId,
                        amountShrimp = pond.amountShrimp,
                        deep = pond.deep,
                        diameter = pond.diameter,
                        status = pond.status
                    };

                    if (pond.originPondId == "")
                    {

                        dt.startDate = pond.startDate;
                    }
                    else
                    {
                        var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.originPondId);
                        if (originPond == null)
                        {
                            throw new BadRequestException("Not Found Pond");
                        }
                        dt.startDate = originPond.startDate;
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

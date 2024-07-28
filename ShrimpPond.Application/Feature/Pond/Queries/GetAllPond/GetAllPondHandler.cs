using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
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
            var ponds = _unitOfWork.pondRepository.FindAll();

            foreach(var pond in ponds)
            {
                var dt = new PondDTO()
                {
                    PondId = pond.PondId,
                    PondTypeName = pond.PondTypeName,
                    OriginPondId = pond.OriginPondId,
                    SeedId = pond.SeedId,
                    StartDate = pond.StartDate,
                    AmountShrimp = pond.AmountShrimp,
                    Deep = pond.Deep,
                    Diameter = pond.Diameter,
                    Status = pond.Status
                };
                data.Add(dt);
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

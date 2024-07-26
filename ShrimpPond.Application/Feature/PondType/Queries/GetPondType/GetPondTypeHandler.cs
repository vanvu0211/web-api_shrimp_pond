using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Queries.GetPondType
{
    public class GetPondTypeHandler: IRequestHandler<GetPondType, List<PondTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetPondType> _logger;

        public GetPondTypeHandler(IUnitOfWork unitOfWork, IAppLogger<GetPondType> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<PondTypeDTO>> Handle(GetPondType request, CancellationToken cancellationToken)
        {
            //query
            var pondTypes = _unitOfWork.pondTypeRepository.FindAll();
            //logging
            _logger.LogInformation("Get location successfully");
            // convert
            var data = _mapper.Map<List<PondTypeDTO>>(pondTypes);
            //return
            return data;
        }
    }
}

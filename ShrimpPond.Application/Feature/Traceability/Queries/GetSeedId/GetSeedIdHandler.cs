﻿using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetSeedId
{
    public class GetSeedIdHandler: IRequestHandler<GetSeedId,List<SeedIdDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetSeedId> _logger;

        public GetSeedIdHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetSeedId> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<SeedIdDTO>> Handle(GetSeedId request, CancellationToken cancellationToken)
        {
            List<SeedIdDTO> result = new List<SeedIdDTO>();

            var ponds = _unitOfWork.pondRepository.FindAll();
            foreach (var pond in ponds) 
            {

                if (pond.SeedId == "" || result.Where(x=>x.SeedId==pond.SeedId).Count()!=0) continue;

                result.Add(new SeedIdDTO() { SeedId = pond.SeedId });
            }

         
            return result;
        }
    }
}

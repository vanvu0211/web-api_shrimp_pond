﻿using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.Machine.Command.CreateMachine;
namespace ShrimpPond.Application.Feature.Machine.Queries.GetALlMachine
{
    public class GetALlMachineHandler : IRequestHandler<GetALlMachine, List<GetALlMachineDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetALlMachine> _logger;
        private readonly IMapper _mapper;

        public GetALlMachineHandler(IUnitOfWork unitOfWork, IAppLogger<GetALlMachine> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetALlMachineDTO>> Handle(GetALlMachine request, CancellationToken cancellationToken)
        {
            var machines = _unitOfWork.machineRepository.FindByCondition(x => x.farmId == request.farmId).ToList();
            List<GetALlMachineDTO> getALlMachineDTOs = new List<GetALlMachineDTO>();

            foreach (var machine in machines)
            {
                var pondIdDatas = _unitOfWork.pondIdRepository.FindByCondition(x => x.machineId == machine.machineId).ToList();
                List<PondId> pondIds = new List<PondId>();
                foreach (var pondIdData in pondIdDatas)
                {
                    var pondId = new PondId()
                    {
                        pondName = pondIdData.pondName,
                        pondId = pondIdData.pondId
                    };
                    pondIds.Add(pondId);
                }

                var GetALlMachineDTO = new GetALlMachineDTO()
                {
                    machineId = machine.machineId,
                    machineName = machine.machineName,
                    pondIds = pondIds,
                    status = machine.status
                };
                getALlMachineDTOs.Add(GetALlMachineDTO);
            }
            _logger.LogInformation("create machine successfully");

            return getALlMachineDTOs;
        }
    }
}

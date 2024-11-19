using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;

namespace ShrimpPond.Application.Feature.TimeSetting.Queries.GetTimeSetting;


public class GetTimeSettingHandler: IRequestHandler<GetTimeSetting, List<GetTimeSettingDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<GetTimeSetting> _logger;

    public GetTimeSettingHandler(IUnitOfWork unitOfWork, IAppLogger<GetTimeSetting> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    public Task<List<GetTimeSettingDto>> Handle(GetTimeSetting request, CancellationToken cancellationToken)
    {
        //query
        var timeSetting = _unitOfWork.timeSettingRepository.FindAll().OrderBy(x => x.TimeSettingId).LastOrDefault();
        var timeSettingObjects = _unitOfWork.timeSettingObjectRepository.FindByCondition(x=>timeSetting != null && x.TimeSettingId == timeSetting.TimeSettingId).ToList();
        
        //logging
        _logger.LogInformation("Get Time Setting successfully");
        // convert
        var data = _mapper.Map<List<GetTimeSettingDto>>(timeSettingObjects);
        //return
        return Task.FromResult(data);
    }
}
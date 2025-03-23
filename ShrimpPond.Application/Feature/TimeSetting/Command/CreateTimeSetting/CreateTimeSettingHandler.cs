using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;

namespace ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

public class CreateTimeSettingHandler: IRequestHandler<CreateTimeSetting, string>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateTimeSettingHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateTimeSetting request, CancellationToken cancellationToken)
    {

        var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
        if (farm == null)
        {
            throw new BadRequestException("Farm not found");
        }

        var timeSettingObjects = new List<TimeSettingObject>();
        foreach (var timeSettingObject in request.timeSettingObjects.Select(timeSetting => new TimeSettingObject() 
                 {
                     index = timeSetting.Index,
                     time = timeSetting.Time,
                 }))
        {
            timeSettingObjects.Add(timeSettingObject);
            _unitOfWork.timeSettingObjectRepository.Add(timeSettingObject);
        }

        var timeSettings = _unitOfWork.timeSettingRepository.FindByCondition(x => x.farmId != request.farmId).ToList();
        foreach(var timeSetting in timeSettings)
        {
            timeSetting.enableFarm = false;
            _unitOfWork.timeSettingRepository.Update(timeSetting);
        }

        var data = new Domain.TimeSetting.TimeSetting()
        {
            timeSettingObjects = timeSettingObjects,
            farmId = farm.farmId,
            enableFarm = true
        };


        _unitOfWork.timeSettingRepository.Add(data);
        await _unitOfWork.SaveChangeAsync();
       
        return "Successfully!";
    }
}
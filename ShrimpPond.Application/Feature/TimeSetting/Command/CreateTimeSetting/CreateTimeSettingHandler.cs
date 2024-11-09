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


        List<TimeSettingObject> timeSettingObjects = new List<TimeSettingObject>();
        foreach (var timeSetting in request.timeSettingObjects)
        {
            var timeSettingObject = new TimeSettingObject() 
            {
                Index = timeSetting.Index,
                Time = timeSetting.Time,
            };
            timeSettingObjects.Add(timeSettingObject);
            _unitOfWork.timeSettingObjectRepository.Add(timeSettingObject);

        }
        var data = new Domain.TimeSetting.TimeSetting() 
        { 
            timeSettingObjects = timeSettingObjects 
        };


        _unitOfWork.timeSettingRepository.Add(data);
        await _unitOfWork.SaveChangeAsync();
       
        return "Succesfully!";
    }
}
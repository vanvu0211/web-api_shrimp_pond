using MediatR;

namespace ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

public class CreateTimeSetting : IRequest< string>
{
    public List<TimeSettingData> timeSettingObjects { get; set; }
}
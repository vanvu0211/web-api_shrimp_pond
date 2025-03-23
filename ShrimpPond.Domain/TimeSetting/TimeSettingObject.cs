
using System.Runtime;
namespace ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

public class TimeSettingObject
{
    public int timeSettingObjectId { get; set; }
    public int index { get; set; }
    public string time { get; set; } = string.Empty;

    public int timeSettingId { get; set; }
    public Domain.TimeSetting.TimeSetting? timeSetting { get; set; }

}


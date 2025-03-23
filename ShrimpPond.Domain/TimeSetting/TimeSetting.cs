using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

namespace ShrimpPond.Domain.TimeSetting
{
    public class TimeSetting
    {
        public int timeSettingId { get; set; }
        public ICollection<TimeSettingObject>? timeSettingObjects { get; set; }

        public bool enableFarm { get; set; }
        public int farmId { get; set; }
        public Domain.Farm.Farm farm { get; set; }
    }
}

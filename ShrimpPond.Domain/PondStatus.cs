using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain
{
    public enum PondStatus
    {
        [Display(Name = "Active")]
        [Description("Active")]
        Active = 0,

        [Display(Name = "NoActive")]
        [Description("NoActive")]
        Inactive = 1,
    }
}

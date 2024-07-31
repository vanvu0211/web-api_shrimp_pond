﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Harves
{
    public enum EHarvest
    {
        [Display(Name = "ApartCollect")]
        [Description("ApartCollect")]
        Active = 0,

        [Display(Name = "TotalCollect")]
        [Description("TotalCollect")]
        Inactive = 1,
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Queries.GetPondType
{
    public class PondTypeDto
    {
        public string PondTypeId { get; set; } = string.Empty;
        public string PondTypeName { get; set; } = string.Empty;
        public string FarmName {  get; set; } = string.Empty;
    }
}

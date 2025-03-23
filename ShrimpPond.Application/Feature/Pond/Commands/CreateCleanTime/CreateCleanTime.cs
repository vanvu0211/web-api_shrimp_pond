﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.CreateCleanTime
{
    public class CreateCleanTime: IRequest<string>
    {
        public DateTime cleanTime {  get; set; }
        public string userName { get; set; } = string.Empty;
        public string farmName { get; set; } = string.Empty;

    }
}

﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetTimeClean
{
    public record GetTimeClean : IRequest<GetTimeCleanDTO>
    {
    }
}

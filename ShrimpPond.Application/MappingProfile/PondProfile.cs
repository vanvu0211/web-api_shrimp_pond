using AutoMapper;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.Collect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class PondProfile: Profile
    {
       public PondProfile() 
       {
            CreateMap<Pond,PondDTO>().ReverseMap();
       }
    }
}

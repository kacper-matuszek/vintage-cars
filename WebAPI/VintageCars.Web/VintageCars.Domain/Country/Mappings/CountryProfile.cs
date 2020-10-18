using System.Collections.Generic;
using AutoMapper;
using Nop.Core;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Country.Response;

namespace VintageCars.Domain.Country.Mappings
{
    public class CountryProfile : Profile, IOrderedMapperProfile
    {
        public CountryProfile()
        {
            CreateMap<Nop.Core.Domain.Directory.Country, CountryView>();
        }

        public int Order => 3;
    }
}

using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Country.StateProvince.Response;

namespace VintageCars.Domain.Country.StateProvince.Mappings
{
    public class StateProvinceProfile : Profile, IOrderedMapperProfile
    {
        public StateProvinceProfile()
        {
            CreateMap<Nop.Core.Domain.Directory.StateProvince, StateProvinceView>();
        }

        public int Order => 4;
    }
}

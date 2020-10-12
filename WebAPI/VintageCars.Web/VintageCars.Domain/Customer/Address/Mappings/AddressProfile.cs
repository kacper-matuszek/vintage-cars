using System;
using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Customer.Address.Commands;

namespace VintageCars.Domain.Customer.Address.Mappings
{
    public class AddressProfile : Profile, IOrderedMapperProfile
    {
        public AddressProfile()
        {
            CreateMap<CreateUpdateAddressCommand, Nop.Core.Domain.Common.Address>()
                .ForMember(a => a.CreatedOnUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(a => a.Id, opt => opt.MapFrom(addr => addr.Id.HasValue ? addr.Id.Value : Guid.NewGuid()));
        }

        public int Order => 2;
    }
}

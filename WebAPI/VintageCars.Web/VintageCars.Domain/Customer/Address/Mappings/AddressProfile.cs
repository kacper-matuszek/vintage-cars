using System;
using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Customer.Address.Commands;
using VintageCars.Domain.Customer.Address.Responses;

namespace VintageCars.Domain.Customer.Address.Mappings
{
    public class AddressProfile : Profile, IOrderedMapperProfile
    {
        public AddressProfile()
        {
            CreateMap<CreateUpdateAddressCommand, Nop.Core.Domain.Common.Address>()
                .ForMember(a => a.CreatedOnUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(a => a.Id, opt => opt.MapFrom(addr => addr.Id.HasValue ? addr.Id.Value : Guid.NewGuid()));

            CreateMap<Nop.Core.Domain.Common.Address, AddressDetailResponse>();
        }

        public int Order => 2;
    }
}

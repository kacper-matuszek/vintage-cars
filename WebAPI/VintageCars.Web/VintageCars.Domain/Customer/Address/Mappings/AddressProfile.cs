using System;
using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Customer.Address.Commands;
using VintageCars.Domain.Customer.Address.Responses;
using VintageCars.Domain.Extensions;

namespace VintageCars.Domain.Customer.Address.Mappings
{
    public class AddressProfile : Profile, IOrderedMapperProfile
    {
        public AddressProfile()
        {
            CreateMap<CreateUpdateAddressCommand, Nop.Core.Domain.Common.Address>()
                .ForMember(a => a.CreatedOnUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .GenerateId();

            CreateMap<Nop.Core.Domain.Common.Address, AddressDetailResponse>();
        }

        public int Order => 2;
    }
}

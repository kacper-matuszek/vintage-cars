using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Core.Requests.Customers;
using VintageCars.Domain.Customer.Commands;

namespace VintageCars.Domain.Customer.Mappings
{
    public class CustomerProfile : Profile, IOrderedMapperProfile
    {
        public CustomerProfile()
        {
            CreateMap<CreateAccountCommand, Nop.Core.Domain.Customers.Customer>()
                .ForMember(d => d.Active, opt => opt.MapFrom(_ => true));
            CreateMap<CreateAccountCommand, CustomerRegistrationRequest>()
                .ForMember(d => d.IsApproved, opt => opt.MapFrom(_ => true))
                .ForMember(d => d.Customer, opt => opt.MapFrom(src => src));
        }
        public int Order => 1;
    }
}

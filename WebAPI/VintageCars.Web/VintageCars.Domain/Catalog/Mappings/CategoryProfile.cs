using System;
using AutoMapper;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Extensions;

namespace VintageCars.Domain.Catalog.Mappings
{
    public class CategoryProfile : Profile, IOrderedMapperProfile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>()
                .GenerateId()
                .ForMember(c => c.CreatedOnUtc,
                    opt =>
                    {
                        opt.Condition((src, dest) => !src.Id.HasValue && dest.CreatedOnUtc == DateTime.MinValue);
                        opt.MapFrom(cat => DateTime.UtcNow);
                    })
                .ForMember(c => c.UpdatedOnUtc,
                    opt => opt.MapFrom(cat => DateTime.UtcNow));
        }
        public int Order => 6;
    }
}

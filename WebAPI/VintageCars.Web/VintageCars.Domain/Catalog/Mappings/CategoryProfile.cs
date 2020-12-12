using System;
using AutoMapper;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Extensions;

namespace VintageCars.Domain.Catalog.Mappings
{
    public class CategoryProfile : Profile, IOrderedMapperProfile
    {
        public CategoryProfile()
        {
            CreateMap<CreateUpdateCategoryCommand, Category>()
                .GenerateId()
                .ForMember(c => c.Published, opt => opt.MapFrom(src => src.IsPublished))
                .ForMember(c => c.CreatedOnUtc,
                    opt =>
                    {
                        opt.Condition((src, dest) => !src.Id.HasValue && dest.CreatedOnUtc == DateTime.MinValue);
                        opt.MapFrom(cat => DateTime.UtcNow);
                    })
                .ForMember(c => c.UpdatedOnUtc,
                    opt => opt.MapFrom(cat => DateTime.UtcNow));

            CreateMap<Category, CategoryView>()
                .ForMember(x => x.IsPublished, opt => opt.MapFrom(src => src.Published))
                .ForMember(x => x.IsArchival, opt => opt.MapFrom(src => src.Deleted))
                .ForMember(x => x.Attributes, opt => opt.Ignore());
        }
        public int Order => 6;
    }
}

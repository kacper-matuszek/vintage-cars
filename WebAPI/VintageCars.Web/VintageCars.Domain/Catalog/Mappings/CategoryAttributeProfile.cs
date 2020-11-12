using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;

namespace VintageCars.Domain.Catalog.Mappings
{
    public class CategoryAttributeProfile : Profile, IOrderedMapperProfile
    {
        public CategoryAttributeProfile()
        {
            CreateMap<CreateCategoryAttributeCommand, CategoryAttribute>();
        }
        public int Order => 5;
    }
}

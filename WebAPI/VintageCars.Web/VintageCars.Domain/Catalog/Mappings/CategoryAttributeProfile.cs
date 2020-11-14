using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Extensions;

namespace VintageCars.Domain.Catalog.Mappings
{
    public class CategoryAttributeProfile : Profile, IOrderedMapperProfile
    {
        public CategoryAttributeProfile()
        {
            CreateMap<CreateCategoryAttributeCommand, CategoryAttribute>()
                .GenerateId();
        }
        public int Order => 5;
    }
}

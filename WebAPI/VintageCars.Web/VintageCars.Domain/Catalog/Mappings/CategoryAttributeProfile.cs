using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Catalog.Request;
using VintageCars.Domain.Extensions;

namespace VintageCars.Domain.Catalog.Mappings
{
    public class CategoryAttributeProfile : Profile, IOrderedMapperProfile
    {
        public CategoryAttributeProfile()
        {
            CreateMap<CreateUpdateCategoryAttributeCommand, CategoryAttribute>()
                .GenerateId();

            CreateMap<CreateUpdateCategoryAttributeMappingCommand, CategoryAttributeMapping>()
                .GenerateId();

            CreateMap<CategoryAttributeValueRequest, CategoryAttributeValue>()
                .GenerateId();
        }
        public int Order => 5;
    }
}

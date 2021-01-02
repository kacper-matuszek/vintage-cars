using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Data.Models;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Catalog.Request;
using VintageCars.Domain.Catalog.Response;
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

            CreateMap<CategoryAttribute, CategoryAttributeView>();
            CreateMap<CategoryAttribute, CategoryAttributeMappingView>()
                .ForMember(dest => dest.CategoryAttributeId, opt => opt.MapFrom(src => src.Id));
            CreateMap<CategoryAttributeValue, CategoryAttributeValueView>();
            CreateMap<CategoryAttributeMapping, CategoryAttributeMappingView>();

            CreateMap<CategoryAttribute, CategoryAttributeFullInfoView>();
            CreateMap<CategoryAttributeMapping, CategoryAttributeFullInfoView>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Values, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
        public int Order => 5;
    }
}

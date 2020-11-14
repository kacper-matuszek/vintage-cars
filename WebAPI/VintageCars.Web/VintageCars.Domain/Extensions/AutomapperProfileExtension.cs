using System;
using AutoMapper;
using Nop.Core;
using VintageCars.Domain.Base;

namespace VintageCars.Domain.Extensions
{
    public static class AutomapperProfileExtension
    {
        public static IMappingExpression<TSource, TDestination> GenerateId<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TSource : IBusinessEntity
            where TDestination : BaseEntity
        {
            return mappingExpression.ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src != null && src.Id.HasValue ? src.Id.Value : Guid.NewGuid()));
        }
    }
}

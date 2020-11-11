using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using VintageCars.Data.Models;
using Nop.Data.Extensions;

namespace VintageCars.Data.Builders.Category
{
    public partial class CategoryAttributeMappingBuilder : NopEntityBuilder<CategoryAttributeMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(CategoryAttributeMapping.CategoryAttributeId)).AsGuid()
                .ForeignKey<CategoryAttribute>()
                .WithColumn(nameof(CategoryAttributeMapping.CategoryId)).AsGuid().ForeignKey<Nop.Core.Domain.Catalog.Category>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using VintageCars.Data.Models;
using Nop.Data.Extensions;


namespace VintageCars.Data.Builders.Category
{
    public partial class CategoryAttributeValueBuilder : NopEntityBuilder<CategoryAttributeValue>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(CategoryAttributeValue.Name)).AsString(400).NotNullable()
                .WithColumn(nameof(CategoryAttributeValue.CategoryAttributeMappingId)).AsGuid().ForeignKey<CategoryAttributeMapping>();
        }
    }
}

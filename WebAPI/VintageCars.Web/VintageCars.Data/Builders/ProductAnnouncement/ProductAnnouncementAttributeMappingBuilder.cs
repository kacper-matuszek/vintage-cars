using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using VintageCars.Data.Models;
using Nop.Data.Extensions;

namespace VintageCars.Data.Builders.ProductAnnouncement
{
    public class ProductAnnouncementAttributeMappingBuilder : NopEntityBuilder<ProductAnnouncementAttributeMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(ProductAnnouncementAttributeMapping.ProductAnnouncementAttributeId)).AsGuid()
                .ForeignKey<ProductAnnouncementAttribute>()
                .WithColumn(nameof(ProductAnnouncementAttributeMapping.ProductAnnouncementId)).AsGuid()
                .ForeignKey<Models.ProductAnnouncement>();
        }
    }
}

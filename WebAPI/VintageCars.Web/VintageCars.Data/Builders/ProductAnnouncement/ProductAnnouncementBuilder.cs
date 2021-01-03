using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using VintageCars.Data.Builders.Utils;

namespace VintageCars.Data.Builders.ProductAnnouncement
{
    public class ProductAnnouncementBuilder : NopEntityBuilder<Models.ProductAnnouncement>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.CreateBaseCreationEntityProperties();
        }
    }
}

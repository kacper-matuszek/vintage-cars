using System.Linq;
using FluentMigrator;
using Nop.Data.Migrations;
using VintageCars.Data.Models;

namespace VintageCars.Data.Migrations
{
    [NopMigration("2021/01/17 17:27:00", "Added IsMain column to product announcement picture mapping")]
    public class CreateColumnIsMainToProductAnnouncement : AutoReversingMigration
    {
        public override void Up()
        {
            var tableName = new VintageCarsNameCompatability().TableNames
                .First(x => x.Key == typeof(ProductAnnouncementPictureMapping))
                .Value;
            Create.Column(nameof(ProductAnnouncementPictureMapping.IsMain))
                .OnTable(tableName)
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);
        }
    }
}

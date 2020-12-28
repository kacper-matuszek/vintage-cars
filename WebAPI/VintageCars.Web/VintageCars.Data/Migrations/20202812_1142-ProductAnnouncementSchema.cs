using FluentMigrator;
using Nop.Data.Migrations;
using VintageCars.Data.Models;

namespace VintageCars.Data.Migrations
{
    [NopMigration("2020/12/28 11:42:00", "Prouct Announcement schema")]
    public class ProductAnnouncementSchema : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public ProductAnnouncementSchema(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<ProductAnnouncement>(Create);
            _migrationManager.BuildTable<ProductAnnouncementAttribute>(Create);
            _migrationManager.BuildTable<ProductAnnouncementAttributeMapping>(Create);
        }
    }
}

using FluentMigrator;
using Nop.Data.Migrations;
using VintageCars.Data.Models;

namespace VintageCars.Data.Migrations
{
    [SkipMigrationOnUpdate]
    [NopMigration("2020/11/11 16:03:00", "VintageCars.Data base schema")]
    public class VintageCarsSchema : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public VintageCarsSchema(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<CategoryAttribute>(Create);
            _migrationManager.BuildTable<CategoryAttributeValue>(Create);
            _migrationManager.BuildTable<CategoryAttributeMapping>(Create);

        }
    }
}

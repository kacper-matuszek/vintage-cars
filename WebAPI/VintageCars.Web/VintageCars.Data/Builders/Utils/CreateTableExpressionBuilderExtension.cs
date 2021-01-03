using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;
using VintageCars.Data.Models.Base;

namespace VintageCars.Data.Builders.Utils
{
    public static class CreateTableExpressionBuilderExtension
    {
        public static CreateTableExpressionBuilder CreateBaseCreationEntityProperties(this CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(BaseCreationEntity.CreatedBy)).AsGuid().ForeignKey<Customer>();
            table.WithColumn(nameof(BaseCreationEntity.CreateDate)).AsDateTime2();

            return table;
        }
    }
}

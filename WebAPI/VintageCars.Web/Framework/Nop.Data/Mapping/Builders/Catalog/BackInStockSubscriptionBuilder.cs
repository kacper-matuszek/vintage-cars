using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Catalog
{
    /// <summary>
    /// Represents a back in stock subscription entity builder
    /// </summary>
    public partial class BackInStockSubscriptionBuilder : NopEntityBuilder<BackInStockSubscription>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(BackInStockSubscription.CustomerId)).AsGuid().ForeignKey<Customer>()
                .WithColumn(nameof(BackInStockSubscription.StoreId)).AsGuid().ForeignKey<Store>()
                .WithColumn(nameof(BackInStockSubscription.ProductId)).AsGuid().ForeignKey<Product>();
        }

        #endregion
    }
}
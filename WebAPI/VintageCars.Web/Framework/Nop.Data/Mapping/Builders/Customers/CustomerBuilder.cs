using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Customers
{
    /// <summary>
    /// Represents a customer entity builder
    /// </summary>
    public partial class CustomerBuilder : NopEntityBuilder<Customer>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Customer.Username)).AsString(1000).Nullable()
                .WithColumn(nameof(Customer.Email)).AsString(1000).Nullable()
                .WithColumn(nameof(Customer.EmailToRevalidate)).AsString(1000).Nullable()
                .WithColumn(nameof(Customer.SystemName)).AsString(400).Nullable()
                .WithColumn(nameof(Customer.VendorId)).AsGuid().ForeignKey<Vendor>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Customer), nameof(Customer.BillingAddressId))).AsGuid().ForeignKey<Address>(onDelete: Rule.None).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Customer), nameof(Customer.ShippingAddressId))).AsGuid().ForeignKey<Address>(onDelete: Rule.None).Nullable();
        }

        #endregion
    }
}
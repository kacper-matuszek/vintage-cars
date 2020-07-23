using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Catalog
{
    /// <summary>
    /// Represents a manufacturer entity builder
    /// </summary>
    public class ManufacturerBuilder : NopEntityBuilder<Manufacturer>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Manufacturer.Name)).AsString(400).NotNullable()
                .WithColumn(nameof(Manufacturer.MetaKeywords)).AsString(400).Nullable()
                .WithColumn(nameof(Manufacturer.MetaTitle)).AsString(400).Nullable()
                .WithColumn(nameof(Manufacturer.PriceRanges)).AsString(400).Nullable()
                .WithColumn(nameof(Manufacturer.PageSizeOptions)).AsString(200).Nullable()
                .WithColumn(nameof(Manufacturer.PictureId)).AsGuid().ForeignKey<Picture>();
        }

        #endregion
    }
}
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Blogs
{
    /// <summary>
    /// Represents a blog comment entity builder
    /// </summary>
    public partial class BlogCommentBuilder : NopEntityBuilder<BlogComment>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(BlogComment.StoreId)).AsGuid().ForeignKey<Store>()
                .WithColumn(nameof(BlogComment.CustomerId)).AsGuid().ForeignKey<Customer>()
                .WithColumn(nameof(BlogComment.BlogPostId)).AsGuid().ForeignKey<BlogPost>();
        }

        #endregion
    }
}
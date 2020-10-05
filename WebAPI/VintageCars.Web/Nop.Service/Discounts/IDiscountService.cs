using System;
using System.Collections.Generic;
using Nop.Core.Domain.Discounts;

namespace Nop.Service.Discounts
{
    public interface IDiscountService
    {
        /// <summary>
        /// Delete discount
        /// </summary>
        /// <param name="discount">Discount</param>
        void DeleteDiscount(Discount discount);

        /// <summary>
        /// Gets a discount
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <returns>Discount</returns>
        Discount GetDiscountById(Guid discountId);

        /// <summary>
        /// Gets all discounts
        /// </summary>
        /// <param name="discountType">Discount type; pass null to load all records</param>
        /// <param name="couponCode">Coupon code to find (exact match); pass null or empty to load all records</param>
        /// <param name="discountName">Discount name; pass null or empty to load all records</param>
        /// <param name="showHidden">A value indicating whether to show expired and not started discounts</param>
        /// <param name="startDateUtc">Discount start date; pass null to load all records</param>
        /// <param name="endDateUtc">Discount end date; pass null to load all records</param>
        /// <returns>Discounts</returns>
        IList<Discount> GetAllDiscounts(DiscountType? discountType = null,
            string couponCode = null, string discountName = null, bool showHidden = false,
            DateTime? startDateUtc = null, DateTime? endDateUtc = null);

        /// <summary>
        /// Gets discounts applied to entity
        /// </summary>
        /// <typeparam name="T">Type based on <see cref="DiscountMapping" /></typeparam>
        /// <param name="entity">Entity which supports discounts (<see cref="IDiscountSupported{T}" />)</param>
        /// <returns>List of discounts</returns>
        IList<Discount> GetAppliedDiscounts<T>(IDiscountSupported<T> entity) where T : DiscountMapping;

        /// <summary>
        /// Inserts a discount
        /// </summary>
        /// <param name="discount">Discount</param>
        void InsertDiscount(Discount discount);

        /// <summary>
        /// Updates the discount
        /// </summary>
        /// <param name="discount">Discount</param>
        void UpdateDiscount(Discount discount);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core.Domain.Discounts;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Caching.Extensions;

namespace Nop.Service.Discounts
{
    public class DiscountService : IDiscountService
    {
        #region Fields

        private readonly IRepository<Discount> _discountRepository;
        private readonly ICacheKeyService _cacheKeyService;
        #endregion

        public DiscountService(IRepository<Discount> discountRepository, ICacheKeyService cacheKeyService)
        {
            _discountRepository = discountRepository;
            _cacheKeyService = cacheKeyService;
        }

        #region Methods
        /// <summary>
        /// Delete discount
        /// </summary>
        /// <param name="discount">Discount</param>
        public virtual void DeleteDiscount(Discount discount)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            _discountRepository.Delete(discount);
        }

        /// <summary>
        /// Gets a discount
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <returns>Discount</returns>
        public virtual Discount GetDiscountById(Guid discountId)
        {
            if (discountId == default)
                return null;

            return _discountRepository.ToCachedGetById(discountId);
        }

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
        public virtual IList<Discount> GetAllDiscounts(DiscountType? discountType = null,
            string couponCode = null, string discountName = null, bool showHidden = false,
            DateTime? startDateUtc = null, DateTime? endDateUtc = null)
        {
            //we load all discounts, and filter them using "discountType" parameter later (in memory)
            //we do it because we know that this method is invoked several times per HTTP request with distinct "discountType" parameter
            //that's why let's access the database only once
            var cacheKey = _cacheKeyService.PrepareKeyForDefaultCache(NopDiscountDefaults.DiscountAllCacheKey
                , showHidden, couponCode ?? string.Empty, discountName ?? string.Empty);

            var query = _discountRepository.Table;

            if (!showHidden)
            {
                query = query.Where(discount =>
                    (!discount.StartDateUtc.HasValue || discount.StartDateUtc <= DateTime.UtcNow) &&
                    (!discount.EndDateUtc.HasValue || discount.EndDateUtc >= DateTime.UtcNow));
            }

            //filter by coupon code
            if (!string.IsNullOrEmpty(couponCode))
                query = query.Where(discount => discount.CouponCode == couponCode);

            //filter by name
            if (!string.IsNullOrEmpty(discountName))
                query = query.Where(discount => discount.Name.Contains(discountName));

            query = query.OrderBy(discount => discount.Name).ThenBy(discount => discount.Id);

            query = query.ToCachedList(cacheKey).AsQueryable();

            //we know that this method is usually invoked multiple times
            //that's why we filter discounts by type and dates on the application layer
            if (discountType.HasValue)
                query = query.Where(discount => discount.DiscountType == discountType.Value);

            //filter by dates
            if (startDateUtc.HasValue)
                query = query.Where(discount =>
                    !discount.StartDateUtc.HasValue || discount.StartDateUtc >= startDateUtc.Value);
            if (endDateUtc.HasValue)
                query = query.Where(discount =>
                    !discount.EndDateUtc.HasValue || discount.EndDateUtc <= endDateUtc.Value);

            return query.ToList();
        }

        /// <summary>
        /// Gets discounts applied to entity
        /// </summary>
        /// <typeparam name="T">Type based on <see cref="DiscountMapping" /></typeparam>
        /// <param name="entity">Entity which supports discounts (<see cref="IDiscountSupported{T}" />)</param>
        /// <returns>List of discounts</returns>
        public virtual IList<Discount> GetAppliedDiscounts<T>(IDiscountSupported<T> entity) where T : DiscountMapping
        {
            var _discountMappingRepository = EngineContext.Current.Resolve<IRepository<T>>();

            return (from d in _discountRepository.Table
                    join ad in _discountMappingRepository.Table on d.Id equals ad.DiscountId
                    where ad.EntityId == entity.Id
                    select d).ToList();
        }

        /// <summary>
        /// Inserts a discount
        /// </summary>
        /// <param name="discount">Discount</param>
        public virtual void InsertDiscount(Discount discount)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            _discountRepository.Insert(discount);
        }

        /// <summary>
        /// Updates the discount
        /// </summary>
        /// <param name="discount">Discount</param>
        public virtual void UpdateDiscount(Discount discount)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            _discountRepository.Update(discount);
        }
        #endregion
    }
}

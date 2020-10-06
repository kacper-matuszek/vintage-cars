using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Service.Caching;
using Nop.Service.Caching.Extensions;

namespace Nop.Service.Shipping
{
    public class ShippingService : IShippingService
    {
        #region Fields
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly ICacheKeyService _cacheKeyService;
        #endregion

        public ShippingService(IRepository<Warehouse> warehouseRepository, ICacheKeyService cacheKeyService)
        {
            _warehouseRepository = warehouseRepository;
            _cacheKeyService = cacheKeyService;
        }

        #region Warehouses

        /// <summary>
        /// Deletes a warehouse
        /// </summary>
        /// <param name="warehouse">The warehouse</param>
        public virtual void DeleteWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            _warehouseRepository.Delete(warehouse);
        }

        /// <summary>
        /// Gets a warehouse
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier</param>
        /// <returns>Warehouse</returns>
        public virtual Warehouse GetWarehouseById(Guid warehouseId)
        {
            if (warehouseId == default)
                return null;

            return _warehouseRepository.ToCachedGetById(warehouseId);
        }

        /// <summary>
        /// Gets all warehouses
        /// </summary>
        /// <param name="name">Warehouse name</param>
        /// <returns>Warehouses</returns>
        public virtual IList<Warehouse> GetAllWarehouses(string name = null)
        {
            var query = from wh in _warehouseRepository.Table
                        orderby wh.Name
                        select wh;

            var warehouses = query.ToCachedList(_cacheKeyService.PrepareKeyForDefaultCache(NopShippingDefaults.WarehousesAllCacheKey));

            if (!string.IsNullOrEmpty(name))
            {
                warehouses = warehouses.Where(wh => wh.Name.Contains(name)).ToList();
            }

            return warehouses;
        }

        /// <summary>
        /// Inserts a warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        public virtual void InsertWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            _warehouseRepository.Insert(warehouse);
        }

        /// <summary>
        /// Updates the warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        public virtual void UpdateWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            _warehouseRepository.Update(warehouse);
        }

        #endregion
    }
}

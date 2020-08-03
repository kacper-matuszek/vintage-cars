using System.Collections.Generic;

namespace Nop.Service.Store
{
    public partial interface IStoreService
    {
        /// <summary>
        /// Deletes a store
        /// </summary>
        /// <param name="store">Store</param>
        void DeleteStore(Core.Domain.Stores.Store store);

        /// <summary>
        /// Gets all stores
        /// </summary>
        /// <returns>Stores</returns>
        IList<Core.Domain.Stores.Store> GetAllStores();

        /// <summary>
        /// Gets a store 
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Store</returns>
        Core.Domain.Stores.Store GetStoreById(int storeId);

        /// <summary>
        /// Inserts a store
        /// </summary>
        /// <param name="store">Store</param>
        void InsertStore(Core.Domain.Stores.Store store);

        /// <summary>
        /// Updates the store
        /// </summary>
        /// <param name="store">Store</param>
        void UpdateStore(Core.Domain.Stores.Store store);

        /// <summary>
        /// Parse comma-separated Hosts
        /// </summary>
        /// <param name="store">Store</param>
        /// <returns>Comma-separated hosts</returns>
        string[] ParseHostValues(Core.Domain.Stores.Store store);

        /// <summary>
        /// Indicates whether a store contains a specified host
        /// </summary>
        /// <param name="store">Store</param>
        /// <param name="host">Host</param>
        /// <returns>true - contains, false - no</returns>
        bool ContainsHostValue(Core.Domain.Stores.Store store, string host);

        /// <summary>
        /// Returns a list of names of not existing stores
        /// </summary>
        /// <param name="storeIdsNames">The names and/or IDs of the store to check</param>
        /// <returns>List of names and/or IDs not existing stores</returns>
        string[] GetNotExistingStores(string[] storeIdsNames);
    }
}
namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class StoreService : IStoreService
    {
        private readonly IDeletableEntityRepository<Store> storeRepository;

        public StoreService(IDeletableEntityRepository<Store> storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var stores = this.storeRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();

            return stores;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var store = this.storeRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return store;
        }

        public int GetCount()
        {
            return this.storeRepository.All().Count();
        }
    }
}

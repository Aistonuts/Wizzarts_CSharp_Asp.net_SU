namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class StoreService : IStoreService
    {
        private readonly IDeletableEntityRepository<Store> storeRepository;

        public StoreService(
            IDeletableEntityRepository<Store> storeRepository)
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
    }
}

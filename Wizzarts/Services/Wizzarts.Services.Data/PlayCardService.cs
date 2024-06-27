namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    using static Wizzarts.Common.GlobalConstants;

    public class PlayCardService : IPlayCardService
    {
        private readonly IDeletableEntityRepository<PlayCard> cardRepository;
        private readonly IMemoryCache cache;

        public PlayCardService(
            IDeletableEntityRepository<PlayCard> cardRepository,
            IMemoryCache cache)
        {
            this.cardRepository = cardRepository;
            this.cache = cache;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var card = this.GetCachedData<T>()
             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
             .ToList();
            return card;
        }

        public int GetCount()
        {
            return this.cardRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.GetCachedData<T>()
            .Take(count)
            .ToList();
        }

        public IEnumerable<T> GetCachedData<T>()
        {
            var cachedCards = this.cache.Get<IEnumerable<T>>(CardsCacheKey);

            if (cachedCards == null)
            {
                cachedCards = this.cardRepository.AllAsNoTracking().Where(x => x.ApprovedByAdmin == true).OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(CardsCacheKey, cachedCards, cacheOptions);
            }

            return cachedCards;
        }
    }
}

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

    public class ArtService : IArtService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Art> artRepository;
        private readonly IMemoryCache cache;

        public ArtService(
            IDeletableEntityRepository<Art> artRepository,
            IMemoryCache cache)
        {
            this.artRepository = artRepository;
            this.cache = cache;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var cachedArt = this.GetCachedData<T>();
            var art = cachedArt
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            return art;
        }

        public int GetCount()
        {
            return this.artRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            var art = this.GetCachedData<T>()
                .OrderBy(x => Guid.NewGuid())

               .Take(count)
               .ToList();

            return art;
        }

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        public IEnumerable<T> GetCachedData<T>()
        {
            var cachedArt = this.cache.Get<IEnumerable<T>>(ArtsCacheKey);

            if (cachedArt == null)
            {
                cachedArt = this.artRepository.AllAsNoTracking().OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(ArtsCacheKey, cachedArt, cacheOptions);
            }

            return cachedArt;
        }
    }
}

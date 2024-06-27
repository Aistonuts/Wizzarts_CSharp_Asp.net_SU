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

    public class ArticleService : IArticleService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IMemoryCache cache;

        public ArticleService(
            IDeletableEntityRepository<Article> articleRepository,
            IMemoryCache cache)
        {
            this.articleRepository = articleRepository;
            this.cache = cache;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var articles = this.GetCachedData<T>()
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();
            return articles;
        }

        public int GetCount()
        {
            return this.articleRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.GetCachedData<T>()
             .Take(count)
             .ToList();
        }

        public IEnumerable<T> GetCachedData<T>()
        {

            var cachedArticles = this.cache.Get<IEnumerable<T>>(ArticlesCacheKey);

            if (cachedArticles == null)
            {
                cachedArticles = this.articleRepository.AllAsNoTracking().OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(ArticlesCacheKey, cachedArticles, cacheOptions);
            }

            return cachedArticles;
        }
    }
}

namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Art;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    using static MagicCardsmith.Common.GlobalConstants;

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

        public async Task ApproveArt(string id)
        {
            var art = this.artRepository.All().FirstOrDefault(x => x.Id == id);
            art.ApprovedByAdmin = true;
            this.cache.Remove(ArtsCacheKey);
            await this.artRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateArtInputModel input, string userId, string imagePath)
        {
            var art = new Art
            {
                Title = input.Title,
                Description = input.Description,
                ApplicationUserId = userId,

            };

            Directory.CreateDirectory($"{imagePath}/art/userArt/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            art.Extension = extension;
            var physicalPath = $"{imagePath}/art/userArt/{art.Id}.{extension}";
            art.RemoteImageUrl = $"/images/art/userArt/{art.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.artRepository.AddAsync(art);
            await this.artRepository.SaveChangesAsync();
            this.cache.Remove(ArtsCacheKey);
        }

        public async Task DeleteAsync(string id)
        {
            var art = this.artRepository.All().FirstOrDefault(x => x.Id == id);
            this.artRepository.Delete(art);
            await this.artRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var cachedArt = this.GetCachedData<T>();


            var art = cachedArt
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            return art;
        }

        public IEnumerable<T> GetAllByArtistId<T>(int id)
        {
            var art = this.artRepository.AllAsNoTracking()
                .Where(x => x.ArtIstId == id)
                .To<T>().ToList();

            return art;
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var art = this.artRepository.AllAsNoTracking()
               .Where(x => x.ApplicationUserId == id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>().ToList();

            return art;
        }

        public T GetById<T>(string id)
        {
            var art = this.artRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

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

        public bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        public async Task UpdateAsync(string id, BaseCreateArtInputModel input)
        {
            var articles = this.artRepository.All().FirstOrDefault(x => x.Id == id);
            articles.Title = input.Title;
            articles.Description = input.Description;
            articles.RemoteImageUrl = input.RemoteImageUrl;

            await this.artRepository.SaveChangesAsync();
            this.cache.Remove(ArtsCacheKey);
        }
    }
}

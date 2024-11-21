namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Art;

    using static Wizzarts.Common.AdminConstants;

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

        public async Task<int> GetCountAsync()
        {
            return await this.artRepository.All().CountAsync();
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
                cachedArt = this.artRepository.AllAsNoTracking().Where(x => x.ApprovedByAdmin == true && x.ForMainPage == true).OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(ArtsCacheKey, cachedArt, cacheOptions);
            }

            return cachedArt;
        }

        public async Task<T> GetById<T>(string id)
        {
            var art = await this.artRepository.AllAsNoTracking()
           .Where(x => x.Id == id)
           .To<T>().FirstOrDefaultAsync();

            return art;
        }

        public async Task AddAsync(AddArtViewModel input, string userId, string imagePath, bool isPremium)
        {
            var art = new Art
            {
                Title = input.Title,
                Description = input.Description,
                AddedByMemberId = userId,
                ForMainPage = isPremium,
            };

            Directory.CreateDirectory($"{imagePath}/art/userArt/");
            var extension = Path.GetExtension(input.Image.FileName)!.TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            art.Extension = extension;
            var physicalPath = $"{imagePath}/art/userArt/{art.Title}.{extension}";
            art.RemoteImageUrl = $"/images/art/userArt/{art.Title}.{extension}";
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.artRepository.AddAsync(art);
            await this.artRepository.SaveChangesAsync();
            this.cache.Remove(ArtsCacheKey);
        }

        public async Task UpdateAsync(EditArtViewModel input, string Id)
        {
            var art = this.artRepository.All().FirstOrDefault(x => x.Id == Id);

            if (art != null)
            {
                art.Title = input.Title;
                art.Description = input.Description;
                await this.artRepository.SaveChangesAsync();
                this.cache.Remove(ArtsCacheKey);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var art = this.artRepository.All().FirstOrDefault(x => x.Id == id);
            if (art != null)
            {
                this.artRepository.Delete(art);
                await this.artRepository.SaveChangesAsync();
                this.cache.Remove(ArtsCacheKey);
            }
        }

        public IEnumerable<T> GetAllArtByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var art = this.artRepository.AllAsNoTracking()
              .Where(x => x.AddedByMemberId == id)
              .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
              .To<T>().ToList();

            return art;
        }

        public IEnumerable<T> GetAllArtByUserIdPaginationless<T>(string id)
        {
            var art = this.artRepository.AllAsNoTracking()
               .Where(x => x.AddedByMemberId == id)
               .To<T>().ToList();

            return art;
        }

        public async Task<string> ApproveArt(string id)
        {
            var art = this.artRepository.All().FirstOrDefault(x => x.Id == id);
            if (art != null && art.ApprovedByAdmin == false)
            {
                art.ApprovedByAdmin = true;
                this.cache.Remove(ArtsCacheKey);
                await this.artRepository.SaveChangesAsync();

                return art.AddedByMemberId;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> ArtExist(string id)
        {

            return await this.artRepository
                .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }

        public async Task<bool> HasUserWithIdAsync(string artId, string userId)
        {
            return await this.artRepository.AllAsNoTracking()
                 .AnyAsync(a => a.Id == artId && a.AddedByMemberId == userId);
        }
    }
}

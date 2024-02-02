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

    using static System.Net.Mime.MediaTypeNames;

    public class ArtService : IArtService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Art> artRepository;

        public ArtService(
            IDeletableEntityRepository<Art> artRepository)
        {
            this.artRepository = artRepository;
        }

        public async Task CreateAsync(CreateArtInputModel input, int artistId, string imagePath)
        {
            var art = new Art
            {
                Title = input.Title,
                Description = input.Description,
                ArtIstId = artistId,
            };

            Directory.CreateDirectory($"{imagePath}/art/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            art.Extension = extension;
            var physicalPath = $"{imagePath}/art/{art.Id}.{extension}";
            art.RemoteImageUrl = $"/images/art/{art.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.artRepository.AddAsync(art);
            await this.artRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var art = this.artRepository.All().FirstOrDefault(x => x.Id == id);
            this.artRepository.Delete(art);
            await this.artRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var art = this.artRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return art;
        }

        public IEnumerable<T> GetAllByArtistId<T>(int id)
        {
            var art = this.artRepository.AllAsNoTracking()
                .Where(x => x.ArtIstId == id)
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
            return this.artRepository.All()
               .OrderBy(x => Guid.NewGuid())
               .Take(count)
               .To<T>().ToList();
        }
    }
}

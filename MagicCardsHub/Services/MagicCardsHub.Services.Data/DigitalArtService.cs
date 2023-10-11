namespace MagicCardsHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;
    using MagicCardsHub.Web.ViewModels.DigitalArt;

    public class DigitalArtService : IDigitalArtService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<DigitalArt> digitalArtRepository;

        public DigitalArtService(IDeletableEntityRepository<DigitalArt> digitalArtRepository)
        {
            this.digitalArtRepository = digitalArtRepository;
        }

        public async Task CreateAsync(CreateDigitalArtInputModel input, string userId, string imagePath)
        {
            var digitalArt = new DigitalArt()
            {
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                ArtIstId = userId,
            };


            if (input.ImageUrl != null)
            {
                var urlImage = new Image
                {
                    AddedByUserId = userId,
                    RemoteImageUrl = input.ImageUrl,
                };

                digitalArt.Images.Add(urlImage);
            }

            if (input.Image != null)
            {
                Directory.CreateDirectory($"{imagePath}/digitalArt/");
                foreach (var image in input.Image)
                {
                    var extension = Path.GetExtension(image.FileName).TrimStart('.');
                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var dbImage = new Image
                    {
                        AddedByUserId = userId,
                        Extension = extension,
                    };
                    digitalArt.Images.Add(dbImage);

                    var physicalPath = $"{imagePath}/digitalArt/{dbImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.digitalArtRepository.AddAsync(digitalArt);
            await this.digitalArtRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var digitalArt = this.digitalArtRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();

            return digitalArt;
        }

        public T GetById<T>(string id)
        {
            var digitArt = this.digitalArtRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return digitArt;
        }
    }
}

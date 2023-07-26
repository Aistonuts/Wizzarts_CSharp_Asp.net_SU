using MagicCardsHub.Data.Models;
using System.Collections.Generic;

namespace MagicCardsHub.Services.Data
{
    using System.IO;
    using System.Linq;
    using System;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Web.ViewModels.DigitalArt;
    using MagicCardsHub.Web.ViewModels.GameProject;

    public class GameProjectService : IGameProjectService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<GameFormatProject> gameFormatRepository;

        public GameProjectService(IDeletableEntityRepository<GameFormatProject> gameFormatRepository)
        {
            this.gameFormatRepository = gameFormatRepository;
        }

        public async Task CreateAsync(CreateGameProjectInputModel input, string userId, string imagePath)
        {
            var gameProject = new GameFormatProject()
            {
                Name = input.Name,
                Description = input.Description,
                NumberOfCards = input.NumberOfCards,
                ProjectCreatorId = userId,

            };


            if (input.ImageUrl != null)
            {
                var urlImage = new Image
                {
                    AddedByUserId = userId,
                    RemoteImageUrl = input.ImageUrl,
                };

                gameProject.Images.Add(urlImage);
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
                    gameProject.Images.Add(dbImage);

                    var physicalPath = $"{imagePath}/digitalArt/{dbImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.gameFormatRepository.AddAsync(gameProject);
            await this.gameFormatRepository.SaveChangesAsync();
        }
    }
}

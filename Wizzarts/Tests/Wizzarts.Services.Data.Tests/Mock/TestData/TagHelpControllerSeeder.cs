

namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class TagHelpControllerSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.TagHelpControllers.Any())
            {
                return;
            }

            await dbContext.TagHelpControllers.AddAsync(new TagHelpController
            {
                Id = "045e115a-8432-4100-b7d3-6588832b3d6e",
                Name = "Article",
            });

            await dbContext.TagHelpControllers.AddAsync(new TagHelpController
            {
                Id = "2ff7d8e4-6dfd-4017-b16d-a852f9932b43",
                Name = "Art",
            });

            await dbContext.TagHelpControllers.AddAsync(new TagHelpController
            {
                Id = "07810f5f-3b38-44ba-858a-ef1bdeae4325",
                Name = "Deck",
            });

            await dbContext.TagHelpControllers.AddAsync(new TagHelpController
            {
                Id = "63c9da9d-5b64-4b08-95a9-b4e5f9ec38b6",
                Name = "PlayCard",
            });

            await dbContext.TagHelpControllers.AddAsync(new TagHelpController
            {
                Id = "8b072d65-5fb5-4d4c-b1e8-e2da7ba495f0",
                Name = "Store",
            });

            await dbContext.TagHelpControllers.AddAsync(new TagHelpController
            {
                Id = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                Name = "Event",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

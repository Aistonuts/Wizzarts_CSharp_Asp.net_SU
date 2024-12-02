namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class PlayCardFrameColorSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.PlayCardFrameColors.Any())
            {
                return;
            }

            await dbContext.PlayCardFrameColors.AddAsync(new PlayCardFrameColor
            {
                Name = "White",
                ImageUrl = "/images/frames/white.png",
            });

            await dbContext.PlayCardFrameColors.AddAsync(new PlayCardFrameColor
            {
                Name = "Blue",
                ImageUrl = "/images/frames/blue.png",
            });

            await dbContext.PlayCardFrameColors.AddAsync(new PlayCardFrameColor
            {
                Name = "Black",
                ImageUrl = "/images/frames/black.png",
            });

            await dbContext.PlayCardFrameColors.AddAsync(new PlayCardFrameColor
            {
                Name = "Red",
                ImageUrl = "/images/frames/red.png",
            });

            await dbContext.PlayCardFrameColors.AddAsync(new PlayCardFrameColor
            {
                Name = "Green",
                ImageUrl = "/images/frames/green.png",
            });

            await dbContext.PlayCardFrameColors.AddAsync(new PlayCardFrameColor
            {
                Name = "Gold",
                ImageUrl = "/images/frames/gold.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

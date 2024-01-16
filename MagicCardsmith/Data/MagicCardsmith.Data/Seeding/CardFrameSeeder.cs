using MagicCardsmith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Data.Seeding
{
    public class CardFrameSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CardFrameColors.Any())
            {
                return;
            }

            await dbContext.CardFrameColors.AddAsync(new CardFrameColor
            {
                Name = "White",
                ImageUrl = "/images/frames/white.png",
            });

            await dbContext.CardFrameColors.AddAsync(new CardFrameColor
            {
                Name = "Blue",
                ImageUrl = "/images/frames/blue.png",
            });

            await dbContext.CardFrameColors.AddAsync(new CardFrameColor
            {
                Name = "Black",
                ImageUrl = "/images/frames/black.png",
            });

            await dbContext.CardFrameColors.AddAsync(new CardFrameColor
            {
                Name = "Red",
                ImageUrl = "/images/frames/red.png",
            });

            await dbContext.CardFrameColors.AddAsync(new CardFrameColor
            {
                Name = "Green",
                ImageUrl = "/images/frames/green.png",
            });

            await dbContext.CardFrameColors.AddAsync(new CardFrameColor
            {
                Name = "Gold",
                ImageUrl = "/images/frames/gold.png",
            });
        }
    }
}

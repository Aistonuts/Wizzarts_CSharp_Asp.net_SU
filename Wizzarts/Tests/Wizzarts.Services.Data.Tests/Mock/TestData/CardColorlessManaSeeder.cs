namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class CardColorlessManaSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.ColorlessMana.Any())
            {
                return;
            }

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 0,
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 1,
                ColorName = "1 mana of any color",
                ImageUrl = "/images/mana/1.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 2,
                ColorName = "2 mana of any color",
                ImageUrl = "/images/mana/2.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 3,
                ColorName = "3 mana of any color",
                ImageUrl = "/images/mana/3.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 4,
                ColorName = "4 mana  of any color",
                ImageUrl = "/images/mana/4.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 5,
                ColorName = "5 mana of any color",
                ImageUrl = "/images/mana/5.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 6,
                ColorName = "6 mana of any color",
                ImageUrl = "/images/mana/6.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 7,
                ColorName = "7 mana of any color",
                ImageUrl = "/images/mana/7.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 8,
                ColorName = "8 mana of any color",
                ImageUrl = "/images/mana/8.png",
            });

            await dbContext.ColorlessMana.AddAsync(new ColorlessMana
            {
                Cost = 9,
                ColorName = "9 mana of any color",
                ImageUrl = "/images/mana/9.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

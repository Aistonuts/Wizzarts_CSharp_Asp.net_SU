namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data.Tests.Mock;

    public class CardBlueManaSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.BlueMana.Any())
            {
                return;
            }

            await dbContext.BlueMana.AddAsync(new BlueMana
            {
                Cost = 0,
            });

            await dbContext.BlueMana.AddAsync(new BlueMana
            {
                Cost = 1,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueMana.AddAsync(new BlueMana
            {
                Cost = 2,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueMana.AddAsync(new BlueMana
            {
                Cost = 3,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueMana.AddAsync(new BlueMana
            {
                Cost = 4,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueMana.AddAsync(new BlueMana
            {
                Cost = 5,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class CardRedManaSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.RedMana.Any())
            {
                return;
            }

            await dbContext.RedMana.AddAsync(new RedMana
            {
                Cost = 0,
            });

            await dbContext.RedMana.AddAsync(new RedMana
            {
                Cost = 1,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedMana.AddAsync(new RedMana
            {
                Cost = 2,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedMana.AddAsync(new RedMana
            {
                Cost = 3,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedMana.AddAsync(new RedMana
            {
                Cost = 4,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedMana.AddAsync(new RedMana
            {
                Cost = 5,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

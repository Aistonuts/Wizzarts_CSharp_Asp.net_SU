namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class CardWhiteManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.WhiteMana.Any())
            {
                return;
            }

            await dbContext.WhiteMana.AddAsync(new WhiteMana
            {
                Cost = 0,
            });

            await dbContext.WhiteMana.AddAsync(new WhiteMana
            {
                Cost = 1,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteMana.AddAsync(new WhiteMana
            {
                Cost = 2,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteMana.AddAsync(new WhiteMana
            {
                Cost = 3,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteMana.AddAsync(new WhiteMana
            {
                Cost = 4,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteMana.AddAsync(new WhiteMana
            {
                Cost = 5,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

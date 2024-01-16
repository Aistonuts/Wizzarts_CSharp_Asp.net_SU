namespace MagicCardsmith.Data.Seeding
{
    using MagicCardsmith.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class WhiteManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.WhiteManas.Any())
            {
                return;
            }

            await dbContext.WhiteManas.AddAsync(new WhiteMana
            {
                Cost = 1,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteManas.AddAsync(new WhiteMana
            {
                Cost = 2,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteManas.AddAsync(new WhiteMana
            {
                Cost = 3,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteManas.AddAsync(new WhiteMana
            {
                Cost = 4,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.WhiteManas.AddAsync(new WhiteMana
            {
                Cost = 5,
                ColorName = "White",
                ImageUrl = "/images/mana/w.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

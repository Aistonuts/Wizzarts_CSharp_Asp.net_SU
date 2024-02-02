namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class BlueManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BlueManas.Any())
            {
                return;
            }

            await dbContext.BlueManas.AddAsync(new BlueMana
            {
                Cost = 0,
            });

            await dbContext.BlueManas.AddAsync(new BlueMana
            {
                Cost = 1,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueManas.AddAsync(new BlueMana
            {
                Cost = 2,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueManas.AddAsync(new BlueMana
            {
                Cost = 3,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueManas.AddAsync(new BlueMana
            {
                Cost = 4,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.BlueManas.AddAsync(new BlueMana
            {
                Cost = 5,
                ColorName = "Blue",
                ImageUrl = "/images/mana/u.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

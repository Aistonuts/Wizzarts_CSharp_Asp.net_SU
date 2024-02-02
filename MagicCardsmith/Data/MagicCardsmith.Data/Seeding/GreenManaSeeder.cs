namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class GreenManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GreenManas.Any())
            {
                return;
            }

            await dbContext.GreenManas.AddAsync(new GreenMana
            {
                Cost = 0,
            });

            await dbContext.GreenManas.AddAsync(new GreenMana
            {
                Cost = 1,
                ColorName = "Green",
                ImageUrl = "/images/mana/g.png",
            });

            await dbContext.GreenManas.AddAsync(new GreenMana
            {
                Cost = 2,
                ColorName = "Green",
                ImageUrl = "/images/mana/g.png",
            });

            await dbContext.GreenManas.AddAsync(new GreenMana
            {
                Cost = 3,
                ColorName = "Green",
                ImageUrl = "/images/mana/g.png",
            });

            await dbContext.GreenManas.AddAsync(new GreenMana
            {
                Cost = 4,
                ColorName = "Green",
                ImageUrl = "/images/mana/g.png",
            });

            await dbContext.GreenManas.AddAsync(new GreenMana
            {
                Cost = 5,
                ColorName = "Green",
                ImageUrl = "/images/mana/g.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

using MagicCardsmith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Data.Seeding
{
    public class RedManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.RedManas.Any())
            {
                return;
            }

            await dbContext.RedManas.AddAsync(new RedMana
            {
                Cost = 0,
            });

            await dbContext.RedManas.AddAsync(new RedMana
            {
                Cost = 1,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedManas.AddAsync(new RedMana
            {
                Cost = 2,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedManas.AddAsync(new RedMana
            {
                Cost = 3,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedManas.AddAsync(new RedMana
            {
                Cost = 4,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.RedManas.AddAsync(new RedMana
            {
                Cost = 5,
                ColorName = "Red",
                ImageUrl = "/images/mana/r.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

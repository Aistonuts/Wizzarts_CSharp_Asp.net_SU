using System;
using System.Linq;
using System.Threading.Tasks;
using Wizzarts.Data.Models;

namespace Wizzarts.Data.Seeding
{
    public class CardBlueManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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

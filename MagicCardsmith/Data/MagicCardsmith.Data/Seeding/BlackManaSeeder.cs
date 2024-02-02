namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class BlackManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BlackManas.Any())
            {
                return;
            }

            await dbContext.BlackManas.AddAsync(new BlackMana
            {
                Cost = 0,
            });

            await dbContext.BlackManas.AddAsync(new BlackMana
            {
                Cost = 1,
                ColorName = "Black",
                ImageUrl = "/images/mana/b.png",
            });

            await dbContext.BlackManas.AddAsync(new BlackMana
            {
                Cost = 2,
                ColorName = "Black",
                ImageUrl = "/images/mana/b.png",
            });

            await dbContext.BlackManas.AddAsync(new BlackMana
            {
                Cost = 3,
                ColorName = "Black",
                ImageUrl = "/images/mana/b.png",
            });

            await dbContext.BlackManas.AddAsync(new BlackMana
            {
                Cost = 4,
                ColorName = "Black",
                ImageUrl = "/images/mana/b.png",
            });

            await dbContext.BlackManas.AddAsync(new BlackMana
            {
                Cost = 5,
                ColorName = "Black",
                ImageUrl = "/images/mana/b.png",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

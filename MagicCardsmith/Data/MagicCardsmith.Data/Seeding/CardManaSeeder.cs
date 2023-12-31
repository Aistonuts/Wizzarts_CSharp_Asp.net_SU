namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class CardManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CardsMana.Any())
            {
                return;
            }

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = 2,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
                CardId = 6,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = 7,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = 7,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = 10,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = 1,
            });
            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = 4,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = 3,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = 3,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = 8,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "One",
                RemoteImageUrl = "/images/mana/1.png",
                CardId = 2,
            });

            await dbContext.CardsMana.AddAsync(new CardMana
            {
                Color = "One",
                RemoteImageUrl = "/images/mana/1.png",
                CardId = 7,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

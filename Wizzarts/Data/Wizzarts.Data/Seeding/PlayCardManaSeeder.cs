namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class PlayCardManaSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ManaInCard.Any())
            {
                return;
            }

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = 2,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
                CardId = 6,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = 7,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = 7,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = 10,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = 1,
            });
            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = 4,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = 3,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = 3,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = 8,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "One",
                RemoteImageUrl = "/images/mana/1.png",
                CardId = 2,
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "One",
                RemoteImageUrl = "/images/mana/1.png",
                CardId = 7,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

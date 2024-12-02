namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class PlayCardManaSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.ManaInCard.Any())
            {
                return;
            }

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Black",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = "f43639ef-5503-4e8a-a75d-5651c645a03d",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Green",
                RemoteImageUrl = "/images/mana/g.png",
                CardId = "632d8f1f-4fdf-4a28-a0b5-6ae083e91580",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = "7e1ef124-3c7f-4318-89b3-18315d7eaf81",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = "7e1ef124-3c7f-4318-89b3-18315d7eaf81",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Red",
                RemoteImageUrl = "/images/mana/r.png",
                CardId = "1b768ded-75fb-4af9-bc79-f53fe2810ef5",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "Blue",
                RemoteImageUrl = "/images/mana/b.png",
                CardId = "c330fecf-61a9-4e03-8052-cd2b9583a251",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = "4ece25e1-5d3a-4109-8d92-74864fc7da8a",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = "4ece25e1-5d3a-4109-8d92-74864fc7da8a",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "White",
                RemoteImageUrl = "/images/mana/w.png",
                CardId = "3f7ff61c-d081-4326-b30d-82c1b51a2010",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "One",
                RemoteImageUrl = "/images/mana/1.png",
                CardId = "f43639ef-5503-4e8a-a75d-5651c645a03d",
            });

            await dbContext.ManaInCard.AddAsync(new ManaInCard
            {
                Color = "One",
                RemoteImageUrl = "/images/mana/1.png",
                CardId = "7e1ef124-3c7f-4318-89b3-18315d7eaf81",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

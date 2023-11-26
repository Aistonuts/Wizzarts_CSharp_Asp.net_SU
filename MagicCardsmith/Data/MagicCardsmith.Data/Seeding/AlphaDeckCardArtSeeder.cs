namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class AlphaDeckCardArtSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Arts.Any())
            {
                return;
            }

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Ancestral recall",
                Description = "Mayas ancestors call from the other world",
                RemoteImageUrl = "/images/art/Alpha/Ancestral_Recall.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId =" ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Bad Moon",
                Description = "Path of peril",
                RemoteImageUrl = "/images/art/Alpha/Bad_Moon.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Crusade",
                Description = "Crusaders call",
                RemoteImageUrl = "/images/art/Alpha/Crusade.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Dark Ritual",
                Description = "Call upon long forgotten knowledge",
                RemoteImageUrl = "/images/art/Alpha/Dark_Ritual.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Forest",
                Description = "The old forest",
                RemoteImageUrl = "/images/art/Forest.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Giant Growth",
                Description = "One's garbage is other rat treasure",
                RemoteImageUrl = "/images/art/Alpha/Giant_Growth.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Goblin King",
                Description = "Only a fool will lead those golbins",
                RemoteImageUrl = "/images/art/Alpha/Goblin_King.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Healing Salve",
                Description = "Where is my pocket healer?",
                RemoteImageUrl = "/images/art/Alpha/Healing_Salve.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Island",
                Description = "Island of your mind",
                RemoteImageUrl = "/images/art/Alpha/Island.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Lightning Bolt",
                Description = "And if lightning could strike more than once at the same spot...",
                RemoteImageUrl = "/images/art/Alpha/Lightning_Bolt.png",
                Extension = ".png",
                AddedById = " ",
                ArtIstId = " ",
                CardId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class AlphaCardArtSeeder : ISeeder
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
                ArtIstId = "1e216571-9c32-4158-9c41-812b5334e696",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Bad Moon",
                Description = "Path of peril",
                RemoteImageUrl = "/images/art/Alpha/Bad_Moon.png",
                Extension = ".png",
                ArtIstId = "33241e2a-ca9a-419f-a604-b52b89db1955",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Crusade",
                Description = "Crusaders call",
                RemoteImageUrl = "/images/art/Alpha/Crusade.png",
                Extension = ".png",
                ArtIstId = "3a5b17b0-03bc-4af3-bb14-16d657a36848",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Dark Ritual",
                Description = "Call upon long forgotten knowledge",
                RemoteImageUrl = "/images/art/Alpha/Dark_Ritual.png",
                Extension = ".png",
                ArtIstId = "3ec4114d-d61b-4c20-b46d-521154de7a53",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Forest",
                Description = "The old forest",
                RemoteImageUrl = "/images/art/Forest.png",
                Extension = ".png",
                ArtIstId = "44ca67de-e366-46db-960a-888a00f2bc3d",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Giant Growth",
                Description = "One's garbage is other rat treasure",
                RemoteImageUrl = "/images/art/Alpha/Giant_Growth.png",
                Extension = ".png",
                ArtIstId = "44ca67de-e366-46db-960a-888a00f2bc3d",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Goblin King",
                Description = "Only a fool will lead those golbins",
                RemoteImageUrl = "/images/art/Alpha/Goblin_King.png",
                Extension = ".png",
                ArtIstId = "45e80fea-cd4a-413e-b3e5-c3b243ab0200",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Healing Salve",
                Description = "Where is my pocket healer?",
                RemoteImageUrl = "/images/art/Alpha/Healing_Salve.png",
                Extension = ".png",
                ArtIstId = "a918104d-e9b0-400c-b146-d74ecb90e90a",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Island",
                Description = "Island of your mind",
                RemoteImageUrl = "/images/art/Alpha/Island.png",
                Extension = ".png",
                ArtIstId = "45fbf99d-778d-4899-9176-c0e86b72404e",

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Title = "Lightning Bolt",
                Description = "And if lightning could strike more than once at the same spot...",
                RemoteImageUrl = "/images/art/Alpha/Lightning_Bolt.png",
                Extension = ".png",
                ArtIstId = "6c4116e9-4f11-44f7-bd37-fa4fc7864e6a",

            });

            await dbContext.SaveChangesAsync();
        }
    }
}

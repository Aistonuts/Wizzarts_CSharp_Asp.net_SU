namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class CardArtSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Arts.Any())
            {
                return;
            }

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "ab8532f9-2a2f-4b65-96f1-90e5468fbed2",
                Title = "Ancestral recall",
                Description = "Mayas ancestors call from the other world",
                RemoteImageUrl = "/images/art/Alpha/Ancestral_Recall.png",
                Extension = ".png",
                ArtIstId = 1,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "61129ce2-5993-44f4-9e2b-acd00ef39c9b",
                Title = "Bad Moon",
                Description = "Path of peril",
                RemoteImageUrl = "/images/art/Alpha/Bad_Moon.png",
                Extension = ".png",
                ArtIstId = 2,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "d4381a9a-094d-4695-b938-9fdbc8e3a35c",
                Title = "Crusade",
                Description = "Crusaders call",
                RemoteImageUrl = "/images/art/Alpha/Crusade.png",
                Extension = ".png",
                ArtIstId = 3,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "b895afd6-18a4-4cb5-b5e4-4f872da33e0f",
                Title = "Dark Ritual",
                Description = "Call upon long forgotten knowledge",
                RemoteImageUrl = "/images/art/Alpha/Dark_Ritual.png",
                Extension = ".png",
                ArtIstId = 4,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "0b678f6d-5b03-4444-a46e-3e56a23ef0af",
                Title = "Forest",
                Description = "The old forest",
                RemoteImageUrl = "/images/art/Alpha/Forest.png",
                Extension = ".png",
                ArtIstId = 5,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "57a28e90-5212-4aac-adca-8501223e4329",
                Title = "Giant Growth",
                Description = "One's garbage is other rat treasure",
                RemoteImageUrl = "/images/art/Alpha/Giant_Growth.png",
                Extension = ".png",
                ArtIstId = 6,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "4ecbc030-6746-4799-a7c3-6dc9da062150",
                Title = "Goblin King",
                Description = "Only a fool will lead those golbins",
                RemoteImageUrl = "/images/art/Alpha/Goblin_King.png",
                Extension = ".png",
                ArtIstId = 7,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "b89e9be9-59c8-427c-a6dd-2f49f87f86d3",
                Title = "Healing Salve",
                Description = "Where is my pocket healer?",
                RemoteImageUrl = "/images/art/Alpha/Healing_Salve.png",
                Extension = ".png",
                ArtIstId = 8,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "593ea234-4739-4ca2-8fde-9bda0b72bb58",
                Title = "Island",
                Description = "Island of your mind",
                RemoteImageUrl = "/images/art/Alpha/Island.png",
                Extension = ".png",
                ArtIstId = 9,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "e1866515-5f21-4289-82fa-c6b1b6c3b9ea",
                Title = "Lightning Bolt",
                Description = "And if lightning could strike more than once at the same spot...",
                RemoteImageUrl = "/images/art/Alpha/Lightning_Bolt.png",
                Extension = ".png",
                ArtIstId = 1,

            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "b2998b68-b7b3-4d0d-8c9f-5c8637007326",
                Title = "Plains",
                Description = "Fields of gold",
                RemoteImageUrl = "/images/art/Alpha/Plains.png",
                Extension = ".png",
                ArtIstId = 9,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "dbf24d21-5ed1-4fdc-bee3-eca4666e9a08",
                Title = "Swamp",
                Description = "Swamp of sorrow",
                RemoteImageUrl = "/images/art/Alpha/Swamp.png",
                Extension = ".png",
                ArtIstId = 9,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "84fbd001-d94b-4b85-9dbb-37b49743e202",
                Title = "Mountain",
                Description = "Rocky mountains",
                RemoteImageUrl = "/images/art/Alpha/Mountain.png",
                Extension = ".png",
                ArtIstId = 9,
            });


            await dbContext.Arts.AddAsync(new Art
            {
                Id = "b1996137-e6f5-45b9-bf5b-96c406560cf0",
                Title = "Black Lotus",
                Description = "All mighty lotus",
                RemoteImageUrl = "/images/art/Unlimited/Black_lotus.jpg",
                Extension = ".jpg",
                ArtIstId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "461a542a-cc20-48cd-9d97-e698d917e797",
                Title = "Mox Emerald",
                Description = "All mighty Emerald",
                RemoteImageUrl = "/images/art/Unlimited/Mox_Emerald.png",
                Extension = ".png",
                ArtIstId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "29c648ab-d9ee-49df-ab88-4e2896c0aa5c",
                Title = "Mox Jet",
                Description = "All mighty Jet",
                RemoteImageUrl = "/images/art/Unlimited/Mox_Jet.png",
                Extension = ".png",
                ArtIstId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "ea669431-b629-45ac-8506-cdfee9d0ef1b",
                Title = "Mox Pearl",
                Description = "All mighty Pearl",
                RemoteImageUrl = "/images/art/Unlimited/Mox_Pearl.png",
                Extension = ".png",
                ArtIstId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                Title = "Mox Ruby",
                Description = "All mighty Ruby",
                RemoteImageUrl = "/images/art/Unlimited/Mox_Ruby.png",
                Extension = ".png",
                ArtIstId = 1,
            });

            await dbContext.Arts.AddAsync(new Art
            {
                Id = "c500e8cb-143c-47d1-a469-82c4f5763bab",
                Title = "Mox Sapphire",
                Description = "All mighty Sapphire",
                RemoteImageUrl = "/images/art/Unlimited/Mox_Sapphire.png",
                Extension = ".png",
                ArtIstId = 1,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class AlphaCardSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Cards.Any())
            {
                return;
            }

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Ancestral Recall",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/ancestral_recall.jpg",
                CardFrameColor = "Blue",
                CardType = "Instant",
                AbilitiesAndFlavor = "Draw 3 cards or force oponent to draw 3 cards.",
                IsEventCard = false,
                ArtId = "5d358d99-c5a6-415b-a604-7768d8d36d1b",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Bad Moon",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/bad-moon.jpg",
                CardFrameColor = "Black",
                CardType = "Enchantement",
                AbilitiesAndFlavor = "All black creatures in play gain +1/+1.",
                IsEventCard = false,
                ArtId = "9e52e42e-0876-41ee-b20f-914f1cb1ae1f",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Crusade",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/crusade.jpg",
                CardFrameColor = "White",
                CardType = "Instant",
                AbilitiesAndFlavor = "All white creatures in play gain +1/+1.",
                IsEventCard = false,
                ArtId = "d7a66a1b-1854-4881-b114-8e7f96e39b87",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Dark Ritual",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/dark-ritual.jpg",
                CardFrameColor = "Black",
                CardType = "Instant",
                AbilitiesAndFlavor = "Add 3 black mana to your pool.",
                IsEventCard = false,
                ArtId = "c56f7928-4994-4e31-9a4d-814c66e6fd24",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Forest",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/forest.jpg",
                CardFrameColor = "Green",
                CardType = "Land",
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                ArtId = "1fcfa377-6e69-445b-b653-cfffec39d9ed",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Gianth Growth",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/giant-growth.jpg",
                CardFrameColor = "Green",
                CardType = "Instant",
                AbilitiesAndFlavor = "Target creature gain +3/+3 until end of turn.",
                IsEventCard = false,
                ArtId = "75c38e4d-69df-4e92-a927-dda83056e80d",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Goblin King",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/goblin-king.jpg",
                CardFrameColor = "Red",
                CardType = "Sorcery",
                AbilitiesAndFlavor = "Goblins in play gain mountainwalk and +1/+1 while this card remains in play.",
                IsEventCard = false,
                ArtId = "6f842a99-66bf-4b98-9f33-3a3a8072e59b",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Healing Salve",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/healing_salve.jpg",
                CardFrameColor = "White",
                CardType = "Instant",
                AbilitiesAndFlavor = "Draw 3 cards or force oponent to draw 3 cards.",
                IsEventCard = false,
                ArtId = "bfa6f905-ef2e-4371-8726-6b0b83385703",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Island",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/island.jpg",
                CardFrameColor = "Blue",
                CardType = "Land",
                AbilitiesAndFlavor = "Tab to add {u} to your mana pool.",
                IsEventCard = false,
                ArtId = "ba25c7a9-d626-4fd4-81b1-f46110450058",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Lightning Bolt",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/lightning-bolt.jpg",
                CardFrameColor = "Red",
                CardType = "Instant",
                AbilitiesAndFlavor = "Lightning bold does 3 damage to one target.",
                IsEventCard = false,
                ArtId = "a6050ff1-53d2-49b6-86f9-d9a6c5d59690",
                GameExpansionId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

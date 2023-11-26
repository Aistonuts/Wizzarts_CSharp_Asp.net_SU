namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    internal class AlphaDeckCardSeeder : ISeeder
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
                BlueManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/ancestral_recall.jpg",
                FrameColor = "blue",

                CardType = "Instant",
                AbilitiesAndFlavor = "Draw 3 cards or force oponent to draw 3 cards.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Bad Moon",
                BlackManaCost = 1,
                ColorlessManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/bad-moon.jpg",
                FrameColor = "black",

                CardType = "Enchantment",
                AbilitiesAndFlavor = "All black creatures in play gain +1/+1.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Crusade",
                WhiteManaCost = 2,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/crusade.jpg",
                FrameColor = "blue",

                CardType = "Instant",
                AbilitiesAndFlavor = "All white creatures in play gain +1/+1.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Dark Ritual",
                BlueManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/dark-ritual.jpg",
                FrameColor = "black",

                CardType = "Interrupt",
                AbilitiesAndFlavor = "Add 3 black mana to your pool.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Forest",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/forest.jpg",
                FrameColor = "green",

                CardType = "Land",
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Gianth Growth",
                GreenManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/giant-growth.jpg",
                FrameColor = "green",

                CardType = "Instant",
                AbilitiesAndFlavor = "Target creature gain +3/+3 until end of turn.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Goblin King",
                RedManaCost = 2,
                ColorlessManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/goblin-king.jpg",
                FrameColor = "blue",

                CardType = "Summon Goblin King",
                AbilitiesAndFlavor = "Goblins in play gain mountainwalk and +1/+1 while this card remains in play.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Healing Salve",
                WhiteManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/healing_salve.jpg",
                FrameColor = "white",

                CardType = "Instant",
                AbilitiesAndFlavor = "Draw 3 cards or force oponent to draw 3 cards.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Island",
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/island.jpg",
                FrameColor = "blue",

                CardType = "Land",
                AbilitiesAndFlavor = "Tab to add {u} to your mana pool.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });

            await dbContext.Cards.AddAsync(new Card
            {
                Name = "Lightning Bolt",
                RedManaCost = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/lightning-bolt.jpg",
                FrameColor = "red",

                CardType = "Instant",
                AbilitiesAndFlavor = "Lightning bold does 3 damage to one target.",
                CardExpansion = "Alpha",
                CardRarity = "Common",
                IsEventCard = false,
                ArtId = " ",
                CardSmithId = " ",
                GameExpansionId = 1,
            });
        }
    }
}
//public string Name { get; set; }
//
//public string RedManaCost { get; set; }
//
//public string GreenManaCost { get; set; }
//
//public string BlueManaCost { get; set; }
//
//public string BlackdManaCost { get; set; }
//
//public string WhiteManaCost { get; set; }
//
//public string ColorlessManaCost { get; set; }
//
//public string CardRemoteUrl { get; set; }
//
//public string FrameColor { get; set; }
//
//public string CardType { get; set; }
//
//public string AbilitiesAndFlavor { get; set; }
//
//public string? Power { get; set; }
//
//public string? Toughness { get; set; }
//
//public string CardExpansion { get; set; }
//
//public string CardRarity { get; set; }
//
//public bool IsEventCard { get; set; } = false;
//
//public string ArtId { get; set; }
//
//public virtual Art Art { get; set; }
//
//public string CardSmithId { get; set; }
//
//public virtual ApplicationUser CardSmith { get; set; }
//
//public virtual int GameExpansionId { get; set; }
//
//public GameExpansion GameExpansion { get; set; }
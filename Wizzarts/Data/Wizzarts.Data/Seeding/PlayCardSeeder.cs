namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class PlayCardSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PlayCards.Any())
            {
                return;
            }

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Ancestral Recall",
                BlueManaId = 2,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/ancestral_recall.jpg",
                CardFrameColorId = 2,
                CardTypeId = 5,
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "ab8532f9-2a2f-4b65-96f1-90e5468fbed2",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Bad Moon",
                BlueManaId = 1,
                BlackManaId = 2,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 2,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/bad-moon.jpg",
                CardFrameColorId = 3,
                CardTypeId = 2,
                AbilitiesAndFlavor = "All black creatures in play gain +1/+1.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "61129ce2-5993-44f4-9e2b-acd00ef39c9b",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Crusade",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 3,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/crusade.jpg",
                CardFrameColorId = 1,
                CardTypeId = 2,
                AbilitiesAndFlavor = "All white creatures in play gain +1/+1.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "d4381a9a-094d-4695-b938-9fdbc8e3a35c",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Dark Ritual",
                BlueManaId = 1,
                BlackManaId = 2,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/dark-ritual.jpg",
                CardFrameColorId = 3,
                CardTypeId = 5,
                AbilitiesAndFlavor = "Add 3 black mana to your pool.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "b895afd6-18a4-4cb5-b5e4-4f872da33e0f",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Forest",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/forest.jpg",
                CardFrameColorId = 5,
                CardTypeId = 3,
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "0b678f6d-5b03-4444-a46e-3e56a23ef0af",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Plains",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/plains.jpg",
                CardFrameColorId = 1,
                CardTypeId = 3,
                AbilitiesAndFlavor = "Tap to add {w} to your mana pool.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "b2998b68-b7b3-4d0d-8c9f-5c8637007326",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Mountain",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/mountain.jpg",
                CardFrameColorId = 4,
                CardTypeId = 3,
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "84fbd001-d94b-4b85-9dbb-37b49743e202",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Swamp",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/swamp.jpg",
                CardFrameColorId = 3,
                CardTypeId = 3,
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "dbf24d21-5ed1-4fdc-bee3-eca4666e9a08",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });
            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Gianth Growth",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 2,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/giant-growth.jpg",
                CardFrameColorId = 5,
                CardTypeId = 5,
                AbilitiesAndFlavor = "Target creature gain +3/+3 until end of turn.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "57a28e90-5212-4aac-adca-8501223e4329",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Goblin King",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 3,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 2,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/goblin-king.jpg",
                CardFrameColorId = 4,
                CardTypeId = 6,
                AbilitiesAndFlavor = "Goblins in play gain mountainwalk and +1/+1 while this card remains in play.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "4ecbc030-6746-4799-a7c3-6dc9da062150",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Healing Salve",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 2,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/healing_salve.jpg",
                CardFrameColorId = 2,
                CardTypeId = 5,
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "b89e9be9-59c8-427c-a6dd-2f49f87f86d3",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Island",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/island.jpg",
                CardFrameColorId = 2,
                CardTypeId = 3,
                AbilitiesAndFlavor = "Tab to add {u} to your mana pool.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "593ea234-4739-4ca2-8fde-9bda0b72bb58",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Lightning Bolt",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 2,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/lightning-bolt.jpg",
                CardFrameColorId = 4,
                CardTypeId = 5,
                AbilitiesAndFlavor = "Lightning bold does 3 damage to one target.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "e1866515-5f21-4289-82fa-c6b1b6c3b9ea",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Black Lotus",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/Black_Lotus.png",
                CardFrameColorId = 6,
                CardTypeId = 1,
                AbilitiesAndFlavor = "Adds 3 mana of any single color of your choice to your mana pool, then is discarded. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "b1996137-e6f5-45b9-bf5b-96c406560cf0",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Mox Emerald",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-emerald.jpg",
                CardFrameColorId = 5,
                CardTypeId = 1,
                AbilitiesAndFlavor = "Add 1 green mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "461a542a-cc20-48cd-9d97-e698d917e797",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Mox Jet",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-jet.jpg",
                CardFrameColorId = 3,
                CardTypeId = 1,
                AbilitiesAndFlavor = "Add 1 black mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "29c648ab-d9ee-49df-ab88-4e2896c0aa5c",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Mox Pearl",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-pearl.jpg",
                CardFrameColorId = 1,
                CardTypeId = 1,
                AbilitiesAndFlavor = "Add 1 white mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "ea669431-b629-45ac-8506-cdfee9d0ef1b",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Mox Ruby",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-ruby.jpg",
                CardFrameColorId = 4,
                CardTypeId = 1,
                AbilitiesAndFlavor = "Add 1 red mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Name = "Mox Sapphire",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-sapphire.jpg",
                CardFrameColorId = 2,
                CardTypeId = 1,
                AbilitiesAndFlavor = "Add 1 blue mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                IsBanned = false,
                ArtId = "c500e8cb-143c-47d1-a469-82c4f5763bab",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

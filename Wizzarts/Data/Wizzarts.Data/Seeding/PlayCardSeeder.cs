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
                Id = "c330fecf-61a9-4e03-8052-cd2b9583a251",
                Name = "Ancestral Recall",
                BlueManaId = 2,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/ancestral_recall.jpg",
                CardFrameColorId = 2,
                CardTypeId = 6,
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                IsEventCard = false,
                ArtId = "ab8532f9-2a2f-4b65-96f1-90e5468fbed2",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "f43639ef-5503-4e8a-a75d-5651c645a03d",
                Name = "Bad Moon",
                BlueManaId = 1,
                BlackManaId = 2,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 2,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/bad-moon.jpg",
                CardFrameColorId = 3,
                CardTypeId = 3,
                AbilitiesAndFlavor = "All black creatures in play gain +1/+1.",
                IsEventCard = false,
                ArtId = "61129ce2-5993-44f4-9e2b-acd00ef39c9b",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "4ece25e1-5d3a-4109-8d92-74864fc7da8a",
                Name = "Crusade",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 3,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/crusade.jpg",
                CardFrameColorId = 1,
                CardTypeId = 3,
                AbilitiesAndFlavor = "All white creatures in play gain +1/+1.",
                IsEventCard = false,
                ArtId = "d4381a9a-094d-4695-b938-9fdbc8e3a35c",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "d4d2ffa1-948d-4c01-b6ae-b3b92a6004f2",
                Name = "Dark Ritual",
                BlueManaId = 1,
                BlackManaId = 2,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/dark-ritual.jpg",
                CardFrameColorId = 3,
                CardTypeId = 6,
                AbilitiesAndFlavor = "Add 3 black mana to your pool.",
                IsEventCard = false,
                ArtId = "b895afd6-18a4-4cb5-b5e4-4f872da33e0f",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "ca5958d7-b51a-49c4-a64c-cefa15bc0575",
                Name = "Forest",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/forest.jpg",
                CardFrameColorId = 5,
                CardTypeId = 4,
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                ArtId = "0b678f6d-5b03-4444-a46e-3e56a23ef0af",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "91c884eb-ca3a-4571-8eee-341eba48029b",
                Name = "Plains",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/plains.jpg",
                CardFrameColorId = 1,
                CardTypeId = 4,
                AbilitiesAndFlavor = "Tap to add {w} to your mana pool.",
                IsEventCard = false,
                ArtId = "b2998b68-b7b3-4d0d-8c9f-5c8637007326",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "52b4336f-7c8d-48e7-bc30-a9f1363339e2",
                Name = "Mountain",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/mountain.jpg",
                CardFrameColorId = 4,
                CardTypeId = 4,
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                ArtId = "84fbd001-d94b-4b85-9dbb-37b49743e202",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "19b87a65-3352-4ee6-9c6a-5b7dfb82bfd1",
                Name = "Swamp",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/swamp.jpg",
                CardFrameColorId = 3,
                CardTypeId = 4,
                AbilitiesAndFlavor = "Tap to add {g} to your mana pool.",
                IsEventCard = false,
                ArtId = "dbf24d21-5ed1-4fdc-bee3-eca4666e9a08",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });
            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "632d8f1f-4fdf-4a28-a0b5-6ae083e91580",
                Name = "Gianth Growth",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 2,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/giant-growth.jpg",
                CardFrameColorId = 5,
                CardTypeId = 6,
                AbilitiesAndFlavor = "Target creature gain +3/+3 until end of turn.",
                IsEventCard = false,
                ArtId = "57a28e90-5212-4aac-adca-8501223e4329",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "7e1ef124-3c7f-4318-89b3-18315d7eaf81",
                Name = "Goblin King",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 3,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 2,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/goblin-king.jpg",
                CardFrameColorId = 4,
                CardTypeId = 8,
                AbilitiesAndFlavor = "Goblins in play gain mountainwalk and +1/+1 while this card remains in play.",
                IsEventCard = false,
                ArtId = "4ecbc030-6746-4799-a7c3-6dc9da062150",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "3f7ff61c-d081-4326-b30d-82c1b51a2010",
                Name = "Healing Salve",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 2,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/healing_salve.jpg",
                CardFrameColorId = 2,
                CardTypeId = 6,
                AbilitiesAndFlavor = "Draw 3 cards or force opponent to draw 3 cards.",
                IsEventCard = false,
                ArtId = "b89e9be9-59c8-427c-a6dd-2f49f87f86d3",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "ea971255-f368-4fa5-adb9-ddf18f58fc6f",
                Name = "Island",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/island.jpg",
                CardFrameColorId = 2,
                CardTypeId = 4,
                AbilitiesAndFlavor = "Tab to add {u} to your mana pool.",
                IsEventCard = false,
                ArtId = "593ea234-4739-4ca2-8fde-9bda0b72bb58",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "1b768ded-75fb-4af9-bc79-f53fe2810ef5",
                Name = "Lightning Bolt",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 2,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Alpha/lightning-bolt.jpg",
                CardFrameColorId = 4,
                CardTypeId = 6,
                AbilitiesAndFlavor = "Lightning bold does 3 damage to one target.",
                IsEventCard = false,
                ArtId = "e1866515-5f21-4289-82fa-c6b1b6c3b9ea",
                CardGameExpansionId = 1,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "94d21dfd-4eeb-4dbd-86db-a2816b9d75a9",
                Name = "Black Lotus",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/Black_Lotus.png",
                CardFrameColorId = 6,
                CardTypeId = 2,
                AbilitiesAndFlavor = "Adds 3 mana of any single color of your choice to your mana pool, then is discarded. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                ArtId = "b1996137-e6f5-45b9-bf5b-96c406560cf0",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "6c0b467f-ce6a-4d90-9ec7-5c3bb8077cca",
                Name = "Mox Emerald",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-emerald.jpg",
                CardFrameColorId = 5,
                CardTypeId = 2,
                AbilitiesAndFlavor = "Add 1 green mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                ArtId = "461a542a-cc20-48cd-9d97-e698d917e797",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "29d331a8-b9a7-4932-adff-6d6dc1c57d9c",
                Name = "Mox Jet",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-jet.jpg",
                CardFrameColorId = 3,
                CardTypeId = 2,
                AbilitiesAndFlavor = "Add 1 black mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                ArtId = "29c648ab-d9ee-49df-ab88-4e2896c0aa5c",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "cd83a0cb-c6d8-40cf-ad85-0aeede8ffd4a",
                Name = "Mox Pearl",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-pearl.jpg",
                CardFrameColorId = 1,
                CardTypeId = 2,
                AbilitiesAndFlavor = "Add 1 white mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                ArtId = "ea669431-b629-45ac-8506-cdfee9d0ef1b",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "940c79ba-4aeb-4abe-bdf5-bb639144c306",
                Name = "Mox Ruby",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-ruby.jpg",
                CardFrameColorId = 4,
                CardTypeId = 2,
                AbilitiesAndFlavor = "Add 1 red mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                ArtId = "c048daf3-f4af-4a03-b65d-d6fc20d18092",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "5f3f96a8-836a-479c-93c8-6921feb79366",
                Name = "Mox Sapphire",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/mox-sapphire.jpg",
                CardFrameColorId = 2,
                CardTypeId = 2,
                AbilitiesAndFlavor = "Add 1 blue mana to your mana pool. Tapping this artifact can be played as an interrupt.",
                IsEventCard = false,
                ArtId = "c500e8cb-143c-47d1-a469-82c4f5763bab",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "4160af89-6994-4f5f-8a37-2df3b85c825c",
                Name = "Bayou",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 1,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/Bayou.png",
                CardFrameColorId = 5,
                CardTypeId = 4,
                AbilitiesAndFlavor = "Counts as both swamp and forest and is affected by spells that affect either.Tap to add either one black or one green to your mana pool",
                IsEventCard = false,
                ArtId = "c500e8cb-143c-47d1-a469-82c4f5763bab",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "9c2f3f79-cef6-4dec-9b55-a55d54b5fb7f",
                Name = "Time Walk",
                BlueManaId = 2,
                BlackManaId = 1,
                RedManaId = 1,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 2,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/Time_Walk.png",
                CardFrameColorId = 2,
                CardTypeId = 7,
                AbilitiesAndFlavor = "Take an extra turn after this one.",
                IsEventCard = false,
                ArtId = "986b5b05-f57e-477d-bb35-c7b024ae343e",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });

            await dbContext.PlayCards.AddAsync(new PlayCard
            {
                Id = "eadcd925-332c-4993-9575-cb1fc9deba8b",
                Name = "Wheel of fortune",
                BlueManaId = 1,
                BlackManaId = 1,
                RedManaId = 2,
                WhiteManaId = 1,
                GreenManaId = 1,
                ColorlessManaId = 3,
                CardRemoteUrl = "/images/cardsByExpansion/Unlimited/Wheel_of_Fortune.png",
                CardFrameColorId = 4,
                CardTypeId = 7,
                AbilitiesAndFlavor = "Both players must discard their hands and draw sevent new cards.",
                IsEventCard = false,
                ArtId = "13285a39-964b-46af-9b91-2e2a62bea2af",
                CardGameExpansionId = 3,
                ApprovedByAdmin = true,
                AddedByMemberId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

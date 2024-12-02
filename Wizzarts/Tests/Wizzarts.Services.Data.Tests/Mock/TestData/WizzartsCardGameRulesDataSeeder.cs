namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    internal class WizzartsCardGameRulesDataSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.WizzartsGameRulesData.Any())
            {
                return;
            }

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Creatures",
                Description = "Creature cards in Magic serve as your primary means to attack, defend, and activate abilities during your turn. They come in a wide array of shapes, sizes, and varying levels of power, so the damage dealt and received by your creatures will depend on these factors. The abilities of creature cards differ based on factors such as color, cost, or the character represented by the card.",
                ImageUrl = "/images/navigation/rulesNav/CreatureRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Artifacts",
                Description = "Use artifact cards to get a strategic edge over your opponent! Artifacts are cards that represent special objects, devices, constructs, equipment, and more! These cards will help you boost your gameplay with special abilities.",
                ImageUrl = "/images/navigation/rulesNav/ArtifactRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Enchantments",
                Description = "Enchantment cards can disrupt your opponent's strategy, protect your own, or even change how your game is played. When you cast an Enchantment card, it enters the battlefield and remains there until it is exiled or destroyed.",
                ImageUrl = "/images/navigation/rulesNav/EnchantmentRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Sorceries",
                Description = "Sorcery cards offer powerful spells that deliver impactful, short-term effects, perfect for disrupting an opponent's strategy or bolstering your own. These cards never enter the battlefield; instead, they proceed directly to the graveyard once their effect unfolds.",
                ImageUrl = "/images/navigation/rulesNav/SorceryRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Instants",
                Description = "Instant cards carry a range of one-shot or short-term effects. These cards can deal damage to a target or allow you to view cards in your library. Instants can be played during your, or your opponent's, turn. Like sorceries, these cards never enter the battlefield; instead, they proceed directly to the graveyard once their effect unfolds.",
                ImageUrl = "/images/navigation/rulesNav/InstantRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Planeswalkers",
                Description = "Harness the power of Magic's greatest with planeswalker cards! These formidable cards can alter the course of your battle by providing reusable abilities that grant you game advantages, such as extra draws, additional life points, or annihilating your opponent's creatures.",
                ImageUrl = "/images/navigation/rulesNav/PlaneswalkerRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });

            await dbContext.WizzartsGameRulesData.AddAsync(new WizzartsGameRulesData
            {
                Title = "Lands",
                Description = "Land cards in Magic Cardsmith represent terrains, environments, and locations from the Multiverse. As the foundation of the game, they produce mana, the vital resource you'll use to cast spells or activate abilities of other cards on the battlefield. You may play one land during your turn, which must occur during the main phase, and it's typically a good idea to play it before casting your spells.",
                ImageUrl = "/images/navigation/rulesNav/LandsRules.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

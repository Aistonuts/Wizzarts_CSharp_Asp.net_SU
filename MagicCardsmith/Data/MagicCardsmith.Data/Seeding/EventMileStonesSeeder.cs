namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

    public class EventMileStonesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.EventMilestones.Any())
            {
                return;
            }

            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "Add abilities and flavour. Give this card a name. Set the card type. Creating a land card will give you one point" +
                "Instant or sorcery type provide two bonus point." +
                "Creating a creature card will gain you 3 bonus points." +
                "Finnaly, Enchantment type of card will gain you 4 points",
                ImageUrl = " ",
                EventId = 1,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "Nmaing your card, setting its type was easy.For better standing you can provide mana cost for each of your cards, power and thoughness" +
                "for each creature card.This will gain you the most points however you will have to test it and explain to us how it will work." +
                "Sharing synergies with other cards will gain you bonus points.",
                ImageUrl = " ",
                EventId = 1,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Demonic tutor.",
                Description = "Search your library for a card and put that card into your hand. Then shuffle your library.",
                ImageUrl = " ",
                EventId = 2,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Wrath of God",
                Description = "Destroy all creatures. They can't be regenerated.",
                ImageUrl = " ",
                EventId = 2,
            });

            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Timetwister",
                Description = "Each player shuffles their hand and graveyard into their library, then draws seven cards. (Then put Timetwister into its owner's graveyard.)",
                ImageUrl = " ",
                EventId = 2,
            });

            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Ankh of Mishra",
                Description = "Whenever a land enters the battlefield, Ankh of Mishra deals 2 damage to that land's controller.",
                ImageUrl = " ",
                EventId = 2,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
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
                Description = "Shady pond under the moonlight",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/Abandoned_Mire.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "The glorious Adeline",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/Adeline.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "Some items can be purchased only by from the shady corners.",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/Black_Market.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "The rise of the empire",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/Empire.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "Almost visible creature creeping in the night.",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/Ghast.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "Fear the uknown.",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/InfernalGrasp.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Add some flavour.",
                Description = "Pesky scavengers",
                ImageUrl = "images/Event/Milestones/EventOneMilestones/Scavengers.png",
                EventId = 1,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Demonic tutor.",
                Description = "Search your library for a card and put that card into your hand. Then shuffle your library.",
                ImageUrl = "images/Event/Milestones/EventTwoMilestones/Demonic_Tutor.png",
                EventId = 2,
                IsCompleted = false,
            });
            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Wrath of God",
                Description = "Destroy all creatures. They can't be regenerated.",
                ImageUrl = "images/Event/Milestones/EventTwoMilestones/Wath_of_God.png",
                EventId = 2,
                IsCompleted = false,
            });

            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Timetwister",
                Description = "Each player shuffles their hand and graveyard into their library, then draws seven cards. (Then put Timetwister into its owner's graveyard.)",
                ImageUrl = "images/Event/Milestones/EventTwoMilestones/Timetwister.png",
                EventId = 2,
                IsCompleted = false,
            });

            await dbContext.EventMilestones.AddAsync(new EventMilestone
            {
                Title = "Ankh of Mishra",
                Description = "Whenever a land enters the battlefield, Ankh of Mishra deals 2 damage to that land's controller.",
                ImageUrl = "images/Event/Milestones/EventTwoMilestones/Ankh_Of_Mishra.png",
                EventId = 2,
                IsCompleted = false,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
namespace Wizzarts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;

    public class EventSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Events.Any())
            {
                return;
            }

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Flavorless cards",
                EventDescription = "There is a set of art pieces without abilities, type or name.Add flavour to them such as type where type can be " +
                "land type, instant card spell type, enchantment type of card, creature type of card with power and toughness." +
                "We will provide you with special tool for participating in the event.",
                RemoteImageUrl = "/images/event/Flavorless.jpg",
                EventStatusId = 1,
                ApprovedByAdmin = true,
                EventCreatorId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Call to arts",
                EventDescription = "We will provide you with set of cards. Your task is attach your art to the card. Only members with art pieces in their profiles can participate",
                RemoteImageUrl = "/images/event/Call_to_arts.jpg",
                EventStatusId = 1,
                ApprovedByAdmin = true,
                EventCreatorId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Arena masters",
                EventDescription = "This event is for those of you interested in becoming arena masters. To become an arena master you need to own a store where tournaments can be hold in future.",
                RemoteImageUrl = "/images/event/ArenaMaster.jpg",
                EventStatusId = 1,
                ApprovedByAdmin = true,
                EventCreatorId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Game play testers.",
                EventDescription = "Create and fill your deck with cards. Click to add or remove a card. Lock your deck when ready and wait for it to be shipped at the desired location." +
                "Selecting Wizzarts store will grant you priority over others. Home delivery is possible but quantities are limited.As a game tester you can play with other at one of our stores." +
                "Select the event you would like to join. We will be waiting for you.",
                RemoteImageUrl = "/images/event/Game_tester.jpg",
                EventStatusId = 1,
                ApprovedByAdmin = true,
                EventCreatorId = "2738e787-5d57-4bc7-b0d2-287242f04695",
            });;

            await dbContext.SaveChangesAsync();
        }
    }
}

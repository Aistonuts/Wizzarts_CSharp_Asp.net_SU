namespace Wizzarts.Services.Data.Tests.Mock
{
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class EventSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
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
                ForMainPage = true,
                ControllerId = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                ActionId = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
                EventCategoryId = 1,
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Call to arts",
                EventDescription = "We will provide you with set of cards. Your task is attach your art to the card. Only members with art pieces in their profiles can participate",
                RemoteImageUrl = "/images/event/Call_to_arts.jpg",
                EventStatusId = 1,
                ApprovedByAdmin = true,
                EventCreatorId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
                ControllerId = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                ActionId = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
                EventCategoryId = 2,
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Arena masters",
                EventDescription = "This event is for those of you interested in becoming arena masters. To become an arena master you need to own a store where tournaments can be hold in future.",
                RemoteImageUrl = "/images/event/ArenaMaster.jpg",
                EventStatusId = 1,
                ApprovedByAdmin = true,
                EventCreatorId = "2738e787-5d57-4bc7-b0d2-287242f04695",
                ForMainPage = true,
                ControllerId = "8b072d65-5fb5-4d4c-b1e8-e2da7ba495f0",
                ActionId = "a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70",
                EventCategoryId = 7,
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
                ForMainPage = false,
                ControllerId = "07810f5f-3b38-44ba-858a-ef1bdeae4325",
                ActionId = "a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70",
                EventCategoryId = 6,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

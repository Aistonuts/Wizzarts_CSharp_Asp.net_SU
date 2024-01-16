namespace MagicCardsmith.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Models;

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
                EventDescription = "There is a set of art pieces without abilities, type or name.To complete this milestone you need to add flavour to them such as card type " +
                "such as land type, instant card spell type, enchantment type of card, creature type of card with power and toughness." +
                "We will provide you with special tool for creating a new play card.",
                RemoteImageUrl = "/images/event/Flavorless.jpg",
                EventStatusId = 1,
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Call to arts",
                EventDescription = "We are providing you a set of flavours. Your taks is to draw an ar piece. Keep in mind that each flavour is for specific type of card" +
                "such as land type, instant card spell type, enchantment type of card, creature type of card with power and toughness." +
                "We will provide you with special tool for creating a new play card.",
                RemoteImageUrl = "/images/event/Call_to_arts.jpg",
                EventStatusId = 1,
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Arena masters",
                EventDescription = "This event is for those of you interested in becoming arena masters. To become an arena master you need to own a store where tournaments can be holded in future.",
                RemoteImageUrl = "/images/event/ArenaMaster.jpg",
                EventStatusId = 1,
            });

            await dbContext.Events.AddAsync(new Event
            {
                Title = "Game play testers.",
                EventDescription = "This event is for those of you interested in testing each one of the cards created during the ongoing events. Teaming up with arena masters will give you more" +
                "access to cards to work with. You will be given access to each one of the newly created cards. All you will have to do is to download each one of the newly created cards, test its mechanics and playstyle during a game and submit your feedback.",
                RemoteImageUrl = "/images/event/Game_tester.jpg",
                EventStatusId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
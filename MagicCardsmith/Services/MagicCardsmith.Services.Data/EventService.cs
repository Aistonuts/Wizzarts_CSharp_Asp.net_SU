namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<EventMilestone> milestonesepository;
        private readonly IDeletableEntityRepository<Card> cardsRepository;

        public EventService(
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<EventMilestone> milestonesepository,
            IDeletableEntityRepository<Card> cardsRepository)
        {
            this.eventRepository = eventRepository;
            this.milestonesepository = milestonesepository;
            this.cardsRepository = cardsRepository;

        }

        public IEnumerable<T> GetAll<T>()
        {
            var events = this.eventRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();

            return events;
        }

        public IEnumerable<T> GetAllEventCards<T>()
        {
            var eventCards = this.cardsRepository.AllAsNoTracking()
                .Where(x => x.IsEventCard == true && x.EventId == 1)
                .To<T>().ToList();

            return eventCards;
        }

        public IEnumerable<T> GetAllMilestones<T>(int id)
        {
            var events = this.milestonesepository.AllAsNoTracking()
                .Where(x => x.EventId == id)
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();

            return events;
        }

        public T GetById<T>(int id)
        {
            var newEvent = this.eventRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return newEvent;
        }

        public T GetMilestoneById<T>(int id)
        {
            var mileStone = this.milestonesepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return mileStone;
        }
    }
}

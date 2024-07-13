namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<EventComponent> eventComponentsRepository;

        public EventService(
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<EventComponent> eventComponentsRepository)
        {
            this.eventRepository = eventRepository;
            this.eventComponentsRepository = eventComponentsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var events = this.eventRepository.AllAsNoTracking()
          .OrderByDescending(x => x.Id)
          .To<T>().ToList();

            return events;
        }

        public IEnumerable<T> GetAllEventComponents<T>(int id)
        {
            var eventComponents = this.eventComponentsRepository.AllAsNoTracking()
               .Where(x => x.EventId == id)
               .OrderByDescending(x => x.Id)
               .To<T>().ToList();

            return eventComponents;
        }

        public IEnumerable<T> GetAllEventsByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var events = this.eventRepository.AllAsNoTracking()
            .Where(x => x.EventCreatorId == id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
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

        public T GetEventComponentById<T>(int id)
        {
            var component = this.eventComponentsRepository.AllAsNoTracking()
               .Where(x => x.Id == id)
               .To<T>().FirstOrDefault();
            return component;
        }
    }
}

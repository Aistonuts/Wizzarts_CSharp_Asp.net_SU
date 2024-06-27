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

        public EventService(IDeletableEntityRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
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
            var events = this.eventComponentsRepository.AllAsNoTracking()
               .Where(x => x.EventId == id)
               .OrderByDescending(x => x.Id)
               .To<T>().ToList();

            return events;
        }
    }
}

namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Event;

    public class EventService : IEventService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<EventComponent> eventComponentsRepository;

        public EventService(
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<EventComponent> eventComponentsRepository)
        {
            this.eventRepository = eventRepository;
            this.eventComponentsRepository = eventComponentsRepository;
        }

        public async Task CreateAsync(CreateEventViewModel input, string userId, string imagePath)
        {
            var newEvent = new Event
            {
                Title = input.Title,
                EventDescription = input.EventDescription,
                EventCreatorId = userId,
                EventStatusId = input.EventStatusId,
            };
            Directory.CreateDirectory($"{imagePath}/event/UserEvent/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/event/UserEvent/{newEvent.Id}.{extension}";
            newEvent.RemoteImageUrl = $"/images/event/UserEvent/{newEvent.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);
            await this.eventRepository.AddAsync(newEvent);
            await this.eventRepository.SaveChangesAsync();

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

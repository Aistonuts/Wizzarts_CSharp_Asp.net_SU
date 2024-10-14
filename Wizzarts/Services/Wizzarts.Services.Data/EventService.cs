namespace Wizzarts.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;

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

        public async Task CreateAsync(CreateEventViewModel input, string userId, string imagePath, bool isContentCreator)
        {
            var newEvent = new Event
            {
                Title = input.Title,
                EventDescription = input.EventDescription,
                EventCreatorId = userId,
                EventStatusId = 1,
                IsContentCreator = isContentCreator,
            };
            Directory.CreateDirectory($"{imagePath}/event/UserEvent/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/event/UserEvent/{newEvent.Title}.{extension}";
            newEvent.RemoteImageUrl = $"/images/event/UserEvent/{newEvent.Title}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);
            await this.eventRepository.AddAsync(newEvent);
            await this.eventRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var currentEvent = this.eventRepository.All().FirstOrDefault(x => x.Id == id);
            if (currentEvent != null)
            {
                this.eventRepository.Delete(currentEvent);
                await this.eventRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> EventExist(int id)
        {
            return await this.eventRepository
                .AllAsNoTracking().AnyAsync(a => a.Id == id);
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

        public async Task<bool> HasUserWithIdAsync(int eventId, string userId)
        {
            return await this.eventRepository.AllAsNoTracking()
                .AnyAsync(a => a.Id == eventId && a.EventCreatorId == userId);
        }

        public async Task UpdateAsync(EditEventViewModel input, int Id)
        {
            var currentEvent = this.eventRepository.All().FirstOrDefault(x => x.Id == Id);

            if (currentEvent != null)
            {
                currentEvent.Title = input.Title;
                currentEvent.EventDescription = input.EventDescription;

                await this.eventRepository.SaveChangesAsync();
            }
        }

        public async Task<string> ApproveEvent(int id)
        {
            var currentEvent = this.eventRepository.All().FirstOrDefault(x => x.Id == id);

            if (currentEvent != null && currentEvent.ApprovedByAdmin == false)
            {
                currentEvent.ApprovedByAdmin = true;
                await this.eventRepository.SaveChangesAsync();
                return currentEvent.EventCreatorId;
            }else
            {
                return null;
            }
        }

        public async Task AddComponentAsync(MyEventSettingsViewModel input, string userId, string imagePath)
        {
            var component = new EventComponent
            {
                Title = input.ComponentTitle,
                Description = input.ComponentDescription,
                EventId = input.EventId,
                RequireArtInput = false,
            };
            if (input.Image != null)
            {
                component.RequireArtInput = true;

                Directory.CreateDirectory($"{imagePath}/event/UserEvent/Components");
                var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var physicalPath = $"{imagePath}/event/UserEvent/Components/{component.Title}.{extension}";
                component.ImageUrl = $"/images/event/UserEvent/Components/{component.Title}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await input.Image.CopyToAsync(fileStream);
            }

            await this.eventComponentsRepository.AddAsync(component);
            await this.eventComponentsRepository.SaveChangesAsync();
        }

        public async Task DeleteComponentAsync(int id)
        {
            var thisEvent = this.eventComponentsRepository.All().FirstOrDefault(x => x.Id == id);
            if (thisEvent != null)
            {
                this.eventComponentsRepository.Delete(thisEvent);
                await this.eventComponentsRepository.SaveChangesAsync();
            };
        }
    }
}

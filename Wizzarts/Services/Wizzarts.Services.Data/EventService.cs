namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
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

        public async Task CreateAsync(CreateEventViewModel input, string userId, string imagePath, bool isContentCreator)
        {
            var newEvent = new Event
            {
                Title = input.Title,
                EventDescription = input.EventDescription,
                EventCreatorId = userId,
                EventStatusId = 1,
                ForMainPage = false,
            };
            Directory.CreateDirectory($"{imagePath}/event/UserEvent/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/event/UserEvent/{newEvent.Title.Replace(" ", "")}.{extension}";
            newEvent.RemoteImageUrl = $"/images/event/UserEvent/{newEvent.Title.Replace(" ", "")}.{extension}";
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);
            await this.eventRepository.AddAsync(newEvent);
            await this.eventRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var currentEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var events = await this.eventRepository.AllAsNoTracking()
                .Where(x => x.ForMainPage == true && x.ApprovedByAdmin == true)
          .OrderByDescending(x => x.Id)
          .To<T>().ToListAsync();

            return events;
        }

        public async Task<IEnumerable<T>> GetAllEventComponents<T>(int id)
        {
            var eventComponents = await this.eventComponentsRepository.AllAsNoTracking()
               .Where(x => x.EventId == id)
               .OrderByDescending(x => x.Id)
               .To<T>().ToListAsync();

            return eventComponents;
        }

        public async Task<IEnumerable<T>> GetAllEventsByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var events = await this.eventRepository.AllAsNoTracking()
            .Where(x => x.EventCreatorId == id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            .To<T>().ToListAsync();

            return events;
        }

        public async Task<T> GetById<T>(int id)
        {
            var newEvent = await this.eventRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();
            return newEvent;
        }

        public async Task<T> GetEventComponentById<T>(int id)
        {
            var component = await this.eventComponentsRepository.AllAsNoTracking()
               .Where(x => x.Id == id)
               .To<T>().FirstOrDefaultAsync();
            return component;
        }

        public async Task<bool> HasUserWithIdAsync(int eventId, string userId)
        {
            return await this.eventRepository.AllAsNoTracking()
                .AnyAsync(a => a.Id == eventId && a.EventCreatorId == userId);
        }

        public async Task UpdateAsync(EditEventViewModel input, int id)
        {
            var currentEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (currentEvent != null)
            {
                currentEvent.Title = input.Title;
                currentEvent.EventDescription = input.EventDescription;

                await this.eventRepository.SaveChangesAsync();
            }
        }

        public async Task<string> ApproveEvent(int id)
        {
            var currentEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (currentEvent != null && currentEvent.ApprovedByAdmin == false)
            {
                currentEvent.ApprovedByAdmin = true;
                await this.eventRepository.SaveChangesAsync();
                return currentEvent.EventCreatorId;
            }

            return null;
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

                var physicalPath = $"{imagePath}/event/UserEvent/Components/{component.Title.Replace(" ", "")}.{extension}";
                component.ImageUrl = $"/images/event/UserEvent/Components/{component.Title.Replace(" ", "")}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await input.Image.CopyToAsync(fileStream);
            }

            await this.eventComponentsRepository.AddAsync(component);
            await this.eventComponentsRepository.SaveChangesAsync();
        }

        public async Task DeleteComponentAsync(int id)
        {
            var thisEvent = await this.eventComponentsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (thisEvent != null)
            {
                this.eventComponentsRepository.Delete(thisEvent);
                await this.eventComponentsRepository.SaveChangesAsync();
            };
        }

        public async Task<bool> EventComponentExist(int id)
        {
            return await this.eventComponentsRepository
               .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }
    }
}

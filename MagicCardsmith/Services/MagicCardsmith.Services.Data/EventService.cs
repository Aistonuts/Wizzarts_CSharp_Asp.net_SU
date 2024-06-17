namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Event;
    using Microsoft.Extensions.Caching.Memory;

    using static MagicCardsmith.Common.GlobalConstants;

    public class EventService : IEventService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<EventMilestone> milestonesepository;
        private readonly IDeletableEntityRepository<Card> cardsRepository;
        private readonly IDeletableEntityRepository<EventStatus> eventStatusRepository;
        private readonly IMemoryCache cache;

        public EventService(
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<EventMilestone> milestonesepository,
            IDeletableEntityRepository<Card> cardsRepository,
            IDeletableEntityRepository<EventStatus> eventStatusRepository,
            IMemoryCache cache)
        {
            this.eventRepository = eventRepository;
            this.milestonesepository = milestonesepository;
            this.cardsRepository = cardsRepository;
            this.eventStatusRepository = eventStatusRepository;
            this.cache = cache;
        }

        public async Task ApproveEvent(int id)
        {
            var currentEvent = this.eventRepository.All().FirstOrDefault(x => x.Id == id);
            currentEvent.ApprovedByAdmin = true;
            await this.eventRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateEventViewModel input, string eventCreator, string imagePath)
        {
            var newEvent = new Event
            {
                Title = input.Title,
                EventDescription = input.EventDescription,
                EventCreatorId = eventCreator,
                RemoteImageUrl = input.ImageUrl,
                EventStatusId = input.StatusId,
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

            // this.cache.Remove(EventsCacheKey);
        }

        public IEnumerable<T> GetAll<T>()
        {
            var events = this.eventRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();

            return events;
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int page, int itemsPerPage = 3)
        {
            var article = this.eventRepository.AllAsNoTracking()
            .Where(x => x.EventCreatorId == id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            .To<T>().ToList();

            return article;
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

        public IEnumerable<T> GetAllStatuses<T>()
        {
            return this.eventStatusRepository.All()
               .To<T>().ToList();
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

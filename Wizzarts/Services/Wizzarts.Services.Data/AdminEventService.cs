﻿namespace Wizzarts.Services.Data
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

    using static Wizzarts.Common.HardCodedConstants;

    public class AdminEventService : IAdminEventService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<TagHelpController> tagHelpControllers;
        private readonly IDeletableEntityRepository<TagHelpAction> tagHelpActions;
        private readonly IDeletableEntityRepository<EventCategory> eventCategories;
        private readonly IDeletableEntityRepository<EventComponent> eventComponentsRepository;

        public AdminEventService(
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<TagHelpController> tagHelpControllers,
            IDeletableEntityRepository<TagHelpAction> tagHelpActions,
            IDeletableEntityRepository<EventCategory> eventCategories,
            IDeletableEntityRepository<EventComponent> eventComponentsRepository)
        {
            this.eventRepository = eventRepository;
            this.tagHelpControllers = tagHelpControllers;
            this.tagHelpActions = tagHelpActions;
            this.eventCategories = eventCategories;
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
                ApprovedByAdmin = true,
                EventCategoryId = input.CategoryId,
            };

            switch (input.CategoryId)
            {
                case FlavorlessType:
                case ImagelessType:
                case ImageType:
                case TextType:
                    newEvent.ControllerId = EventControllerId;
                    newEvent.ActionId = ByIdActionId;
                    break;
                case CreateArticleType:
                    newEvent.ControllerId = ArticleControllerId;
                    newEvent.ActionId = CreateActionId;
                    break;
                case AddArtType:
                    newEvent.ControllerId = ArtControllerId;
                    newEvent.ActionId = AddActionId;
                    break;
                case AddPlayCardType:
                    newEvent.ControllerId = PlayCardControllerId;
                    newEvent.ActionId = AddActionId;
                    break;
                case CreateDeckType:
                    newEvent.ControllerId = DeckControllerId;
                    newEvent.ActionId = CreateActionId;
                    break;
                case CreateEventType:
                    newEvent.ControllerId = EventControllerId;
                    newEvent.ActionId = CreateActionId;
                    break;
                case AddStoreType:
                    newEvent.ControllerId = StoreControllerId;
                    newEvent.ActionId = CreateActionId;
                    break;
                default:
                    newEvent.ControllerId = EventControllerId;
                    newEvent.ActionId = ByIdActionId;
                    break;
            }

            Directory.CreateDirectory($"{imagePath}/event/UserEvent/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/event/UserEvent/{newEvent.Title.Replace(" ", string.Empty)}.{extension}";
            newEvent.RemoteImageUrl = $"/images/event/UserEvent/{newEvent.Title.Replace(" ", string.Empty)}.{extension}";
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
                var eventComponents = this.eventComponentsRepository.AllAsNoTracking()
                    .Where(x => x.EventId == id);

                foreach (var component in eventComponents)
                {
                    this.eventComponentsRepository.Delete(component);
                }

                await this.eventComponentsRepository.SaveChangesAsync();
                this.eventRepository.Delete(currentEvent);
                await this.eventRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> EventExist(int id)
        {
            return await this.eventRepository
                .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage = 3)
        {
            var events = await this.eventRepository.AllAsNoTracking()
          .OrderByDescending(x => x.Id)
          .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
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

        public async Task<bool> HasUserWithIdAsync(int articleId, string userId)
        {
            return await this.eventRepository.AllAsNoTracking()
                .AnyAsync(a => a.Id == articleId && a.EventCreatorId == userId);
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
            var currentEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == input.EventId);
            var component = new EventComponent
            {
                Title = input.ComponentTitle,
                Description = input.ComponentDescription,
                EventId = input.EventId,
                EventCategoryId = currentEvent.EventCategoryId,
            };

            if (currentEvent.EventCategoryId == FlavorlessType || currentEvent.EventCategoryId == ImagelessType)
            {
                component.ControllerId = PlayCardControllerId;
            }
            else
            {
                component.ControllerId = EventControllerId;
            }

            component.ActionId = CreateActionId;

            if (currentEvent.EventCategoryId == FlavorlessType || currentEvent.EventCategoryId == ImagelessType || currentEvent.EventCategoryId == ImageType)
            {
                Directory.CreateDirectory($"{imagePath}/event/UserEvent/Components");
                var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var physicalPath = $"{imagePath}/event/UserEvent/Components/{component.Title.Replace(" ", string.Empty)}.{extension}";
                component.ImageUrl = $"/images/event/UserEvent/Components/{component.Title.Replace(" ", string.Empty)}.{extension}";
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
            }
        }

        public async Task<bool> EventComponentExist(int id)
        {
            return await this.eventComponentsRepository
               .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllTagHelpControllers<T>()
        {
            var controllers = await this.tagHelpControllers.AllAsNoTracking()
          .To<T>().ToListAsync();

            return controllers;
        }

        public async Task<IEnumerable<T>> GetAllTagHelpActions<T>()
        {
            var actions = await this.tagHelpActions.AllAsNoTracking()
            .Where(x => x.Name != "ById")
          .To<T>().ToListAsync();

            return actions;
        }

        public async Task<IEnumerable<T>> GetAllEventCategories<T>()
        {
            var categories = await this.eventCategories.AllAsNoTracking()
          .To<T>().ToListAsync();

            return categories;
        }

        public async Task<bool> EventCategoryExist(int id)
        {
            return await this.eventCategories
              .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }

        public async Task<bool> EventTypeRequireArt(int id)
        {
            var thisEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            return thisEvent.EventCategoryId == FlavorlessType || thisEvent.EventCategoryId == ImagelessType || thisEvent.EventCategoryId == ImageType;
        }

        public async Task<string> PromoteEvent(int id)
        {
            var currentEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (currentEvent != null && currentEvent.ForMainPage == false)
            {
                currentEvent.ForMainPage = true;
                await this.eventRepository.SaveChangesAsync();

                return currentEvent.EventCreatorId;
            }

            return null;
        }

        public Task<int> GetCount()
        {
            return this.eventRepository.All().CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllPaginationless<T>()
        {
            var events = await this.eventRepository.AllAsNoTracking()
          .OrderByDescending(x => x.Id)
          .To<T>().ToListAsync();

            return events;
        }

        public async Task<IEnumerable<T>> GetAllEventsByUserIdPageless<T>(string id)
        {
            var events = await this.eventRepository.AllAsNoTracking()
           .Where(x => x.EventCreatorId == id)
           .To<T>().ToListAsync();

            return events;
        }
    }
}

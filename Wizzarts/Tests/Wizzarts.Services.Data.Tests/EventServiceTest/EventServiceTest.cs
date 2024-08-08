using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data.Repositories;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Event;
using Xunit;

namespace Wizzarts.Services.Data.Tests.EventServiceTest
{
    public class EventServiceTest : UnitTestBase
    {
        public EventServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateEventShouldChangeTheTotalCountOfEventsAndShouldAddTheCorrectEvent()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository,eventComponentsRepository);

            string UserId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, UserId, path);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            this.TearDownBase();
        }


        [Fact]
        public async Task CreateEventWithWrongFileFormatShouldThrowInvalidImageExtensionError()
        {
            OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository, eventComponentsRepository);

            string UserId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(testEvent, UserId, path));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetAllShouldReturnCorrectEventCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository, eventComponentsRepository);
            var events = service.GetAll<EventInListViewModel>();
            int eventCount = events.Count();
            Assert.Equal(4, eventCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetAllEventComponentsShouldReturnCorrectComponentsCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository, eventComponentsRepository);
            var components = service.GetAllEventComponents<EventComponentsInListViewModel>(1);
            int componentsCount = components.Count();
            Assert.Equal(7, componentsCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetAllEventsByUserIdShouldReturnCorrectEventCountAndFirstEventCorrecTitle()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository, eventComponentsRepository);
            var events = service.GetAllEventsByUserId<EventInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695",1,3);
            int eventsCount = events.Count();
            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(3, eventsCount);
            Assert.Equal("Flavorless cards", firstEvent.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetEventByIdShouldReturnTheCorrectEventTitle()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository, eventComponentsRepository);
            var events = service.GetById<SingleEventViewModel>(1);
            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(firstEvent.Title,events.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetEventComponentByIdShouldReturnTheCorrectEventAndDescription()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            var service = new EventService(eventRepository, eventComponentsRepository);
            var components = service.GetEventComponentById<EventComponentsInListViewModel>(1);

            Assert.Equal("Shady pond under the moonlight", components.Description);

            this.TearDownBase();
        }
    }
}

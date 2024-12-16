namespace Wizzarts.Services.Data.Tests.EventServiceTest
{
    using System;
    using System.Drawing.Imaging;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Caching.Memory;
    using Moq;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Data;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Event;
    using Xunit;

    public class EventServiceTest : UnitTestBase
    {
        public EventServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateEventShouldChangeTheTotalCountOfEventsAndShouldAddTheCorrectEvent()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task CreateEventWithWrongFileFormatShouldThrowInvalidImageExtensionError()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.nft")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(testEvent, userId, path, isContentCreator));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetAllShouldReturnCorrectEventCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var events = await service.GetAll<EventInListViewModel>();
            int eventCount = events.Count();
            Assert.Equal(4, eventCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetAllEventComponentsShouldReturnCorrectComponentsCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var components = await service.GetAllEventComponents<EventComponentsInListViewModel>(1);
            int componentsCount = components.Count();
            Assert.Equal(7, componentsCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetAllEventsByUserIdShouldReturnCorrectEventCountAndFirstEventCorrecTitle()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var events = await service.GetAllEventsByUserId<EventInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695", 1, 3);
            int eventsCount = events.Count();
            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(3, eventsCount);
            Assert.Equal("Flavorless cards", firstEvent.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task Event_Get_All_Events_By_Users_Where_Event_Is_Not_For_Main_Page_Should_Return_Correct_Event_Count_And_First_Event_Correct_Title()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var events = await service.GetAllEventsByUsers<EventInListViewModel>();
            int eventsCount = events.Count();
            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(1, eventsCount);
            Assert.Equal("Flavorless cards", firstEvent.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task Promote_Should_Change_For_Main_Page_To_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 4);
            var statusBefore = firstEvent.ForMainPage;

            var events = await service.PromoteEvent(4);
            var statusAfter = firstEvent.ForMainPage;
            Assert.False(statusBefore);
            Assert.True(statusAfter);
            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetEventByIdShouldReturnTheCorrectEventTitle()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var events = await service.GetById<SingleEventViewModel>(1);
            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(firstEvent.Title, events.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task EventGetEventComponentByIdShouldReturnTheCorrectEventAndDescription()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var components = await service.GetEventComponentById<EventComponentsInListViewModel>(1);

            Assert.Equal("Shady pond under the moonlight", components.Description);

            this.TearDownBase();
        }

        [Fact]
        public async Task AddEventComponentShouldChangeTheTotalCountAndShouldAddTheCorrectEventComponentAndItsRequirementForImageShouldBeTrue()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                Image = file,
                EventId = 1,
                ComponentDescription = "test",
            };

            await service.AddComponentAsync(testEvent, userId, path);

            var count = await eventComponentsRepository.All().Where(x => x.EventId == 1).CountAsync();
            var newComponent = data.EventComponents.FirstOrDefault(x => x.Title == "The newest Component");
            Assert.Equal(8, count);
            Assert.Equal(testEvent.ComponentTitle, newComponent.Title);

            //Assert.True(newComponent.RequireArtInput);
            this.TearDownBase();
        }

        [Fact]
        public async Task AddEventComponentWithWrongFileFormatShouldThrowException()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.nft")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Event",
                Image = file,
                ComponentDescription = "test",
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => service.AddComponentAsync(testEvent, userId, path));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task AddEventComponentShouldChangeTheTotalCountAndShouldAddTheCorrectEventComponentAndItsRequirementForImageShouldBeFalse()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                EventId = 1,
                ComponentDescription = "test",
            };

            await service.AddComponentAsync(testEvent, userId, path);

            var count = await eventComponentsRepository.All().Where(x => x.EventId == 1).CountAsync();
            var newComponent = data.EventComponents.FirstOrDefault(x => x.Title == "The newest Component");
            Assert.Equal(8, count);
            Assert.Equal(testEvent.ComponentTitle, newComponent.Title);

            //Assert.False(newComponent.RequireArtInput);
            this.TearDownBase();
        }

        [Fact]
        public async Task ApproveNewEventShouldChangeItsStatusToApprovedTrue()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            bool currentEventStatus = newTestEvent.ApprovedByAdmin;
            await service.ApproveEvent(newTestEvent.Id);
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.False(currentEventStatus);
            Assert.True(newTestEvent.ApprovedByAdmin);
            this.TearDownBase();
        }

        [Fact]
        public async Task ApproveApprovedEventShouldReturnNull()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            newTestEvent.ApprovedByAdmin = true;
            Assert.Null(await service.ApproveEvent(newTestEvent.Id));
            this.TearDownBase();
        }

        [Fact]
        public async Task ApproveNonExistentEventShouldReturnNull()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            Assert.Null(await service.ApproveEvent(15));
            this.TearDownBase();
        }

        [Fact]
        public async Task UpdateEventShouldEditTheCorrectEvent()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);
            var testEvent = new EditEventViewModel()
            {
                Title = "Test",
                EventDescription = "New Update",
                EventStatusId = 1,
            };

            await service.UpdateAsync(testEvent, 1);

            var newTestEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(testEvent.EventDescription, newTestEvent.EventDescription);
            Assert.Equal(testEvent.EventStatusId, newTestEvent.EventStatusId);
            this.TearDownBase();
        }

        [Fact]
        public async Task DeleteComponentShouldRemoveTheCorrectComponentAndChangeTheTotalCount()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                EventId = 1,
                ComponentDescription = "test",
            };

            await service.AddComponentAsync(testEvent, userId, path);

            var count = await eventComponentsRepository.All().Where(x => x.EventId == 1).CountAsync();
            var newComponent = data.EventComponents.FirstOrDefault(x => x.Title == "The newest Component");
            await service.DeleteComponentAsync(newComponent.Id);
            var newCount = await eventComponentsRepository.All().Where(x => x.EventId == 1).CountAsync();
            var componentNotFound = data.EventComponents.Any(x => x.Title == "The newest Component");
            Assert.Equal(8, count);
            Assert.False(componentNotFound);
            Assert.Equal(7, newCount);
            this.TearDownBase();
        }

        [Fact]
        public async Task DeleteNewEventShouldRemoveTheCorrectEvent()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            await service.DeleteAsync(newTestEvent.Id);
            var eventNotFound = data.Events.Any(x => x.Title == "The newest Event");
            var newCount = await eventRepository.All().CountAsync();
            Assert.Equal(5, count);
            Assert.False(eventNotFound);
            Assert.Equal(4, newCount);

            this.TearDownBase();
        }

        [Fact]
        public async Task NewlyAddedEventShouldExist()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            var newEventExist = await service.EventExist(newTestEvent.Id);
            Assert.True(newEventExist);

            this.TearDownBase();
        }

        [Fact]
        public async Task NewlyAddedEventShouldHaveUserWithCorrectId()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            //var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            //IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            Bitmap bitmapImage = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, 50, 50);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }

            var imageStream = new MemoryStream(byteArray);
            var file = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            bool isContentCreator = false;
            var testEvent = new CreateEventViewModel()
            {
                Title = "The newest Event",
                Image = file,
                EventDescription = "test",
                EventStatusId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            var eventHasUserWithId = await service.HasUserWithIdAsync(newTestEvent.Id, userId);
            Assert.True(eventHasUserWithId);

            this.TearDownBase();
        }

        [Fact]
        public async Task CheckingForExistingEventComponentShouldReturnTrue()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var fileService = new FileService();
            var service = new EventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository, fileService);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                EventId = 1,
                ComponentDescription = "test",
            };

            await service.AddComponentAsync(testEvent, userId, path);

            var newTestEvent = data.EventComponents.FirstOrDefault(x => x.Title == "The newest Component");
            Assert.True(await service.EventComponentExist(newTestEvent.Id));
            this.TearDownBase();
        }
    }
}
namespace Wizzarts.Services.Data.Tests.AdminEventServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.TagHelper;
    using Xunit;
    [Collection("Realm tests")]
    public class AdminEventServiceTest : UnitTestBase
    {
        public AdminEventServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Ome()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(1, newTestEvent.EventCategoryId);
            Assert.Equal("4c78da1b-5bfb-4f7a-92de-77d80295863e", newTestEvent.ControllerId);
            Assert.Equal("cf3494ef-93cc-42d2-bf8d-fc733adc3973", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Two()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 2,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(2, newTestEvent.EventCategoryId);
            Assert.Equal("4c78da1b-5bfb-4f7a-92de-77d80295863e", newTestEvent.ControllerId);
            Assert.Equal("cf3494ef-93cc-42d2-bf8d-fc733adc3973", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Three()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 3,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(3, newTestEvent.EventCategoryId);
            Assert.Equal("045e115a-8432-4100-b7d3-6588832b3d6e", newTestEvent.ControllerId);
            Assert.Equal("a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Four()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 4,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(4, newTestEvent.EventCategoryId);
            Assert.Equal("63c9da9d-5b64-4b08-95a9-b4e5f9ec38b6", newTestEvent.ControllerId);
            Assert.Equal("22011e99-c794-4fc3-b277-fb446b253d4c", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Five()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 5,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(5, newTestEvent.EventCategoryId);
            Assert.Equal("2ff7d8e4-6dfd-4017-b16d-a852f9932b43", newTestEvent.ControllerId);
            Assert.Equal("22011e99-c794-4fc3-b277-fb446b253d4c", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Six()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 6,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(6, newTestEvent.EventCategoryId);
            Assert.Equal("07810f5f-3b38-44ba-858a-ef1bdeae4325", newTestEvent.ControllerId);
            Assert.Equal("a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Seven()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 7,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(7, newTestEvent.EventCategoryId);
            Assert.Equal("8b072d65-5fb5-4d4c-b1e8-e2da7ba495f0", newTestEvent.ControllerId);
            Assert.Equal("a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70", newTestEvent.ActionId);
            this.TearDownBase();
        }

        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Eight()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 8,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(8, newTestEvent.EventCategoryId);
            Assert.Equal("4c78da1b-5bfb-4f7a-92de-77d80295863e", newTestEvent.ControllerId);
            Assert.Equal("a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Niine()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 9,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(9, newTestEvent.EventCategoryId);
            Assert.Equal("4c78da1b-5bfb-4f7a-92de-77d80295863e", newTestEvent.ControllerId);
            Assert.Equal("cf3494ef-93cc-42d2-bf8d-fc733adc3973", newTestEvent.ActionId);
            this.TearDownBase();
        }


        [Fact]
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_Category_Ten()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");
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
                CategoryId = 10,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal(10, newTestEvent.EventCategoryId);
            Assert.Equal("4c78da1b-5bfb-4f7a-92de-77d80295863e", newTestEvent.ControllerId);
            Assert.Equal("cf3494ef-93cc-42d2-bf8d-fc733adc3973", newTestEvent.ActionId);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

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
        public async Task Create_Event_Should_Change_The_Total_Count_Of_Events_And_Should_Add_The_Correct_Event_With_Controllers_And_Actions_For_Unknown_Category()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());
            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

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
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
            Assert.Equal("4c78da1b-5bfb-4f7a-92de-77d80295863e", newTestEvent.ControllerId);
            Assert.Equal("cf3494ef-93cc-42d2-bf8d-fc733adc3973", newTestEvent.ActionId);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var events = await service.GetAll<EventInListViewModel>(1, 4);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var events = await service.GetAllEventsByUserId<EventInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695", 1, 3);
            int eventsCount = events.Count();
            var firstEvent = data.Events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(3, eventsCount);
            Assert.Equal("Flavorless cards", firstEvent.Title);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var components = await service.GetEventComponentById<EventComponentsInListViewModel>(1);

            Assert.Equal("Shady pond under the moonlight", components.Description);

            this.TearDownBase();
        }

        [Fact]
        public async Task Add_Event_Component_Should_Change_The_Total_Count_And_Should_Add_The_Correct_Event_Component_And_Its_Requirement_For_Image_Should_Be_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            var newComponent = await data.EventComponents.FirstOrDefaultAsync(x => x.Title == "The newest Component");
            Assert.Equal(8, count);
            Assert.Equal(testEvent.ComponentTitle, newComponent.Title);

            // Assert.True(newComponent.RequireArtInput);
            this.TearDownBase();
        }

        [Fact]
        public async Task Add_Event_Component_With_Wrong_File_Format_Should_Throw_Exception()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.nft");

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
                ComponentTitle = "The newest Component",
                EventId = 1,
                Image = file,
                ComponentDescription = "test",
                EventCategoryId = 1,
                ControllerId = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                ActionId = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => service.AddComponentAsync(testEvent, userId, path));
            Assert.Equal("Invalid image extension nft", exception.Message);
            this.TearDownBase();
        }

        [Fact]
        public async Task Add_Event_Component_Should_Change_The_Total_Count_Of_Components_And_Should_Add_The_Correct_Event_Component_And_Its_Requirement_For_Image_Should_Be_False()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                EventId = 1,
                Image = file,
                ComponentDescription = "test",
                EventCategoryId = 1,
                ControllerId = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                ActionId = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
            };

            await service.AddComponentAsync(testEvent, userId, path);

            var count = await eventComponentsRepository.AllAsNoTracking().Where(x => x.EventId == 1).CountAsync();
            var newComponent = await data.EventComponents.FirstOrDefaultAsync(x => x.Title == "The newest Component");
            Assert.Equal(8, count);
            Assert.Equal("The newest Component", newComponent.Title);

            // Assert.False(newComponent.RequireArtInput);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
                CategoryId = 1,
            };

            await service.CreateAsync(testEvent, userId, path, isContentCreator);

            var count = await eventRepository.All().CountAsync();
            var newTestEvent = data.Events.FirstOrDefault(x => x.Title == "The newest Event");
            bool currentEventStatus = newTestEvent.ApprovedByAdmin;
            await service.ApproveEvent(newTestEvent.Id);
            Assert.Equal(5, count);
            Assert.Equal(testEvent.Title, newTestEvent.Title);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var testEvent = new EditEventViewModel()
            {
                Title = "Test",
                EventDescription = "New Update",
                EventStatusId = 1,
            };

            await service.UpdateAsync(testEvent, 1);

            var newTestEvent = await data.Events.FirstOrDefaultAsync(x => x.Id == 1);
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                EventId = 1,
                Image = file,
                ComponentDescription = "test",
                EventCategoryId = 1,
                ControllerId = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                ActionId = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            // var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            // IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

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
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            string userId = "66030199-349f-4e35-846d-97685187a565";
            string path = $"c:\\Users\\Cmpt\\Downloads\\ASPNetCore\\ASP.NET_try\\Wizzarts\\Web\\Wizzarts.Web\\wwwroot" + "/images";

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var testEvent = new MyEventSettingsViewModel()
            {
                ComponentTitle = "The newest Component",
                EventId = 1,
                Image = file,
                ComponentDescription = "test",
                EventCategoryId = 1,
                ControllerId = "4c78da1b-5bfb-4f7a-92de-77d80295863e",
                ActionId = "cf3494ef-93cc-42d2-bf8d-fc733adc3973",
            };

            await service.AddComponentAsync(testEvent, userId, path);

            var newTestEvent = data.EventComponents.FirstOrDefault(x => x.Title == "The newest Component");
            Assert.True(await service.EventComponentExist(newTestEvent.Id));
            this.TearDownBase();
        }

        [Fact]
        public async Task Check_If_Event_Category_Exist()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            Assert.True(await service.EventCategoryExist(1));
            Assert.False(await service.EventCategoryExist(20));
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_Total_Events_In_Db_Should_Return_Correct_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            Assert.Equal(4, await service.GetCount());
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_Total_TagHelp_Action_In_Db_Should_Return_Correct_Count_And_Tag_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var tagHelpActions = await service.GetAllTagHelpActions<SingleTagHelperActionViewModel>();
            var firstTagHelpTitle = tagHelpActions.FirstOrDefault(x => x.Id == "a1e33d52-660d-4cc9-b6b9-c04ba4b9ec70");
            Assert.Equal(2, tagHelpActions.Count());
            Assert.Equal("Create", firstTagHelpTitle.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_Total_TagHelp_Controller_In_Db_Should_Return_Correct_Count_And_Tag_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var tagHelpActions = await service.GetAllTagHelpControllers<SingleTagHelpControllerViewModel>();
            var firstTagHelpTitle = tagHelpActions.FirstOrDefault(x => x.Id == "045e115a-8432-4100-b7d3-6588832b3d6e");
            Assert.Equal(6, tagHelpActions.Count());
            Assert.Equal("Article", firstTagHelpTitle.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_Total_Event_Categories_In_Db_Should_Return_Correct_Count_And_Tag_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var eventCategories = await service.GetAllEventCategories<EventCategoryInListViewModel>();
            var firstEventCategory = eventCategories.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(10, eventCategories.Count());
            Assert.Equal("Flavorless", firstEventCategory.Title);
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_All_Events_Without_Pagination_Should_Return_Correct_Count_And_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            var events = await service.GetAllPaginationless<EventInListViewModel>();
            var fistEvent = events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal("Flavorless cards", fistEvent.Title);
            Assert.Equal(4, events.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_All_Events_By_User_Id_Without_Pagination_Should_Return_Correct_Count_And_Name()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            var events = await service.GetAllEventsByUserIdPageless<EventInListViewModel>("2738e787-5d57-4bc7-b0d2-287242f04695");
            var fistEvent = events.FirstOrDefault(x => x.Id == 1);
            Assert.Equal("Flavorless cards", fistEvent.Title);
            Assert.Equal(4, events.Count());
            this.TearDownBase();
        }

        [Fact]
        public async Task Checking_If_Event_Type_Require_Art_Should_Return_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            Assert.True(await service.EventTypeRequireArt(1));
            this.TearDownBase();
        }

        [Fact]
        public async Task Checking_If_Event_Type_Require_Art_Should_Return_False()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);
            var result = await service.EventTypeRequireArt(4);
            Assert.False(result);
            this.TearDownBase();
        }

        [Fact]
        public async Task Promote_Event_Should_Change_For_Main_Page_Propterty_To_True()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            var cache = new MemoryCache(new MemoryCacheOptions());

            using var eventRepository = new EfDeletableEntityRepository<Event>(data);
            using var eventComponentsRepository = new EfDeletableEntityRepository<EventComponent>(data);
            using var eventTagHelpControllerRepository = new EfDeletableEntityRepository<TagHelpController>(data);
            using var eventTagHelpActionRepository = new EfDeletableEntityRepository<TagHelpAction>(data);
            using var eventCategoryRepository = new EfDeletableEntityRepository<EventCategory>(data);
            var service = new AdminEventService(eventRepository, eventTagHelpControllerRepository, eventTagHelpActionRepository, eventCategoryRepository, eventComponentsRepository);

            var fistEvent = data.Events.FirstOrDefault(x => x.Id == 4);

            var firstEventBefore = fistEvent.ForMainPage;
            await service.PromoteEvent(4);

            var firstEventAfter = fistEvent.ForMainPage;
            Assert.False(firstEventBefore);
            Assert.True(firstEventAfter);

            this.TearDownBase();
        }
    }
}

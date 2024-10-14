namespace Wizzarts.Services.Data.Tests.ChatServiceTest
{
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Chat;
    using Xunit;

    public class ChatServiceTest : UnitTestBase
    {
        public ChatServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task ChatGetAllMessagesFromChatIdShouldReturnCorrectChatAndMessagesCount()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage,repositoryChat);
            var chat = chatService.GetAllGeneralChatMessages<DbChatMessagesInListViewModel>(1);
            int chatCount = chat.Count();
            var firstChatMessage = chat.First();
            Assert.Equal(1, chatCount);
            Assert.Equal("General",firstChatMessage.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllChatRoomsShouldReturnCorrectCountAndSecondRoomShouldBeFrench()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var rooms = chatService.GetAllChatRooms<SingleChatViewModel>();
            var secondRoom = rooms.FirstOrDefault(x => x.Id == 2);
            int roomsCount = rooms.Count();
            Assert.Equal(8, roomsCount);
            Assert.Equal(secondRoom.Name, "French");
            this.TearDownBase();
        }

        [Fact]
        public async Task GetAllChatMessagesInRoomShouldReturnCorrectCountAndMessageContent()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var chat = chatService.GetAllChatMessagesInChatRoom<DbChatMessagesInListViewModel>(1);
            int chatCount = chat.Count();
            var firstChatMessage = chat.First();
            var message = "Welcome to Wizzarts General chat.";
            Assert.Equal(1, chatCount);
            Assert.Equal("General", firstChatMessage.Name);
            Assert.Equal(message, firstChatMessage.Text);
            this.TearDownBase();
        }

        [Fact]
        public async Task GetChatByIdShouldReturnCorrectChat()
        {
            OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var chat = chatService.GetById<SingleChatViewModel>(1);
            var message = "Welcome to Wizzarts General chat.";
            Assert.Equal(1, chat.Id);
            Assert.Equal("General", chat.Name);
            this.TearDownBase();
        }
    }
}

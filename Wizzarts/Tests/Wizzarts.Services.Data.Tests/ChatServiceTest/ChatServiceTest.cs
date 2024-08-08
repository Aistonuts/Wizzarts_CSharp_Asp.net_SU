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

namespace Wizzarts.Services.Data.Tests.ChatServiceTest
{
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

            using var repositoryChat = new EfDeletableEntityRepository<ChatMessage>(data);
            var chatService = new ChatService(repositoryChat);
            var chat = chatService.GetAllGeneralChatMessages<DbChatMessagesInListViewModel>(1);
            int chatCount = chat.Count();
            var firstChatMessage = chat.First();
            Assert.Equal(1, chatCount);
            Assert.Equal("General",firstChatMessage.Name);
            this.TearDownBase();
        }
    }
}

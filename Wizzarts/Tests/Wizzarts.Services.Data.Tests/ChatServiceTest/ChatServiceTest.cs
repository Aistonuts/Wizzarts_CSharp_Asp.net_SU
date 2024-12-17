namespace Wizzarts.Services.Data.Tests.ChatServiceTest
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;
    using Wizzarts.Data.Repositories;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels;
    using Wizzarts.Web.ViewModels.Chat;
    using Xunit;

    public class ChatServiceTest : UnitTestBase
    {
        public ChatServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task Chat_Get_All_Messages_From_Chat_Id_Should_Return_Correct_Chat_And_Messages_Count()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var chat = await chatService.GetAllGeneralChatMessages<DbChatMessagesInListViewModel>(1);
            int chatCount = chat.Count();
            var firstChatMessage = chat.First();
            Assert.Equal(1, chatCount);
            Assert.Equal("General", firstChatMessage.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_All_Chat_Rooms_Should_Return_Correct_Count_And_Second_Room_Should_Be_For_French_Language()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var rooms = await chatService.GetAllChatRooms<SingleChatViewModel>();
            var secondRoom = rooms.FirstOrDefault(x => x.Id == 2);
            int roomsCount = rooms.Count();
            Assert.Equal(8, roomsCount);
            Assert.Equal("French", secondRoom.Name);
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_All_Chat_Messages_In_Room_Should_Return_Correct_Count_And_Message_Content()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var chat = await chatService.GetAllChatMessagesInChatRoom<DbChatMessagesInListViewModel>(1);
            int chatCount = chat.Count();
            var firstChatMessage = chat.First();
            var message = "Welcome to Wizzarts General chat.";
            Assert.Equal(1, chatCount);
            Assert.Equal("General", firstChatMessage.Name);
            Assert.Equal(message, firstChatMessage.Text);
            this.TearDownBase();
        }

        [Fact]
        public async Task Get_Chat_By_Id_Should_Return_Correct_Chat()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            using var repositoryChatMessage = new EfDeletableEntityRepository<ChatMessage>(data);
            using var repositoryChat = new EfDeletableEntityRepository<Chat>(data);
            var chatService = new ChatService(repositoryChatMessage, repositoryChat);
            var chat = await chatService.GetById<SingleChatViewModel>(1);
            var message = "Welcome to Wizzarts General chat.";
            Assert.Equal(1, chat.Id);
            Assert.Equal("General", chat.Name);
            this.TearDownBase();
        }
    }
}

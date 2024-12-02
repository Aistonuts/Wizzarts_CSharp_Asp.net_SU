namespace Wizzarts.Web.Tests.ControllerTest
{
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Wizzarts.Services.Data.Tests;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.ViewModels.Chat;
    using Xunit;

    public class ChatControllerTest : UnitTestBase
    {
        [Fact]
        public void PostShouldReturnOk()
        {
            this.OneTimeSetup();
            var data = this.dbContext;
            MyController<ChatController>
                .Instance(instance => instance
                    .WithUser(x => x.WithIdentifier("2738e787-5d57-4bc7-b0d2-287242f04695"))
                    .WithData(data.Users))
                .Calling(c => c.Post(
                    new MessageViewModel
                    {
                        ChatId = 1,
                        Text = "Test",
                    }))
                .ShouldReturn()
                .Ok();
            this.TearDownBase();
        }

        [Fact]
        public void ByIdShouldReturnView()
        {
            this.OneTimeSetup();
            var data = this.dbContext;

            MyController<ChatController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(data.Chats.FirstOrDefault(x => x.Id == 1)))
                .Calling(c => c.ById(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleChatViewModel>());

            this.TearDownBase();
        }
    }
}

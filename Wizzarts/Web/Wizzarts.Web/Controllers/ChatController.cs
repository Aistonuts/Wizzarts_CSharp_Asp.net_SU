using Ganss.Xss;

namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Data;
    using Wizzarts.Data.Models;
    using Wizzarts.Data.Models.Enums;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels.Chat;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : BaseController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IChatService chatService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatController(
            ApplicationDbContext dbContext,
            IChatService chatService,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.chatService = chatService;
            this.userManager = userManager;
        }

        // SignalR will show the message, this will save it in db and then will replace the signalR generated message
        [HttpPost]
        public async Task<IActionResult> Post(MessageViewModel viewModel)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var sanitizer = new HtmlSanitizer();
                sanitizer.AllowedTags.Clear();
                var sanitizedText = sanitizer.Sanitize(viewModel.Text);
                var message = new ChatMessage
                {
                    ChatId = viewModel.ChatId,
                    Text = sanitizedText,
                    Timestamp = DateTime.Now,
                };
                if (user.Nickname.Length > 0)
                {
                    message.Name = user.Nickname;

                }
                else
                {
                    message.Name = user.UserName;
                }

                _ = this.dbContext.ChatMessages.AddAsync(message);
                await this.dbContext.SaveChangesAsync();
                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        // not used
        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            try
            {
                var chat = new Chat
                {
                    Name = name,
                    Type = ChatType.Event,
                };
                var user = await this.userManager.GetUserAsync(this.User);
                chat.Users.Add(new ChatUser
                {
                    UserId = user.Id,
                });

                this.dbContext.Chats.Add(chat);

                await this.dbContext.SaveChangesAsync();

                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        // not used
        [HttpPost]
        public async Task<IActionResult> JoinRoom(int id)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var chatUser = new ChatUser
                {
                    ChatId = id,
                    UserId = user.Id,
                };

                this.dbContext.ChatUsers.Add(chatUser);

                await this.dbContext.SaveChangesAsync();
                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        // this will redirect user to new page with all chats and this one will be the current one
        public async Task<IActionResult> ById(int id)
        {
            var view = await this.chatService.GetById<SingleChatViewModel>(id);
            if (view == null)
            {
                return this.BadRequest();
            }

            view.ChatId = id;
            view.ChatRooms = await this.chatService.GetAllChatRooms<SingleChatViewModel>();
            view.Messages = await this.chatService.GetAllChatMessagesInChatRoom<DbChatMessagesInListViewModel>(id);

            return this.View(view);
        }
    }
}

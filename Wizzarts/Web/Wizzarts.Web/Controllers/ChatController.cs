using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Wizzarts.Data;
using Wizzarts.Data.Models.Enums;
using Wizzarts.Data.Models;
using Wizzarts.Web.ViewModels.Votes;
using Wizzarts.Web.ViewModels.Chat;
using Microsoft.AspNetCore.Identity;
using Wizzarts.Web.ViewModels.Art;
using System.Linq;
using Wizzarts.Services.Data;

namespace Wizzarts.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post( MessageViewModel viewModel)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var Message = new ChatMessage
                {
                    ChatId = viewModel.ChatId,
                    Text = viewModel.Text,
                    Name = user.UserName,
                    Timestamp = DateTime.Now,
                };
                this.dbContext.ChatMessages.AddAsync(Message);
                await this.dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
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

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
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
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult ById(int id)
        {
            var view = this.chatService.GetById<SingleChatViewModel>(id);
            view.ChatId = id;
            view.ChatRooms = this.chatService.GetAllChatRooms<SingleChatViewModel>();
            view.Messages = this.chatService.GetAllChatMessagesInChatRoom<DbChatMessagesInListViewModel>(id);

            return this.View(view);
        }
    }
}

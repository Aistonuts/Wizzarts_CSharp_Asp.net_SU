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

namespace Wizzarts.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : BaseController
    {
        private readonly ApplicationDbContext dbContext;

        public ChatController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post( MessageViewModel viewModel)
        {
            try
            {
                var Message = new ChatMessage
                {
                    ChatId = viewModel.ChatId,
                    Text = viewModel.Text,
                    Name = "General",
                    Timestamp = DateTime.Now,
                }; ;
                this.dbContext.ChatMessages.AddAsync(Message);
                await this.dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

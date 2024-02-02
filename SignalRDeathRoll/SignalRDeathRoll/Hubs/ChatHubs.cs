using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRDeathRoll.Models.Chat;

namespace SignalRDeathRoll.Hubs
{
    public class ChatHubs : Hub
    {
        [Authorize]
        public class ChatHub : Hub
        {
            public async Task Send(string message)
            {
                await this.Clients.All.SendAsync(
                    "NewMessage",
                    new Message { User = this.Context.User.Identity.Name, Text = message, });
            }
        }
    }
}

namespace Wizzarts.Web.SignalRChatApp.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Wizzarts.Web.ViewModels.Chat;

    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new MessageViewModel { User = this.Context.User.Identity.Name, Text = message, });
        }
    }
}

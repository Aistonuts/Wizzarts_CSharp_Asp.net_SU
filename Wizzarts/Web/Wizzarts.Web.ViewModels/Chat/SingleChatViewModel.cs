namespace Wizzarts.Web.ViewModels.Chat
{
    using System.Collections.Generic;

    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleChatViewModel : IndexAuthenticationViewModel, IMapFrom<Chat>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<DbChatMessagesInListViewModel> Messages { get; set; }
    }
}

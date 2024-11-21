namespace Wizzarts.Web.ViewModels.Chat
{
    using System;

    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class DbChatMessagesInListViewModel : IMapFrom<ChatMessage>
    {
        public string Name { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }
    }
}

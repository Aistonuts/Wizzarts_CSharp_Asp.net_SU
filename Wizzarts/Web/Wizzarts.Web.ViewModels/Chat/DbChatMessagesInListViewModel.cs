using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.Chat
{
    public class DbChatMessagesInListViewModel : IMapFrom<ChatMessage>
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }
    }
}

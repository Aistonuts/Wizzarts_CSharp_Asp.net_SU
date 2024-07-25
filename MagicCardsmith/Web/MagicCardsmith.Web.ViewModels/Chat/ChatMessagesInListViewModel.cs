using MagicCardsmith.Data.Models;
using MagicCardsmith.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Web.ViewModels.Chat
{
    public class ChatMessagesInListViewModel : IMapFrom<Message>
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }
    }
}

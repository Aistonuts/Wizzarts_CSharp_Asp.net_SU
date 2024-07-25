using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Web.ViewModels.Chat
{
    public class ChatMessagesListViewModel
    {
        public IEnumerable<ChatMessagesInListViewModel> Messages { get; set; }
    }
}

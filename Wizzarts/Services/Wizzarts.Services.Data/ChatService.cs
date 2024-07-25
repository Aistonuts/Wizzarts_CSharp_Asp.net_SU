using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Repositories;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Services.Data
{
    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<ChatMessage> chatMessageRepository;

        public ChatService(IDeletableEntityRepository<ChatMessage> chatMessageRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
        }

        public IEnumerable<T> GetAllGeneralChatMessages<T>(int id = 1)
        {
            var messages = this.chatMessageRepository.AllAsNoTracking()
                .Where(x => x.ChatId == id)
                .To<T>().ToList();

            return messages;
        }
    }
}

namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<ChatMessage> chatMessageRepository;

        private readonly IDeletableEntityRepository<Chat> chatRepository;

        public ChatService(
            IDeletableEntityRepository<ChatMessage> chatMessageRepository,
            IDeletableEntityRepository<Chat> chatRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
            this.chatRepository = chatRepository;

        }

        public IEnumerable<T> GetAllChatMessagesInChatRoom<T>(int id)
        {
            var messages = this.chatMessageRepository.AllAsNoTracking()
                .Where(x => x.ChatId == id)
                .To<T>().ToList();

            return messages;
        }

        public IEnumerable<T> GetAllChatRooms<T>()
        {
            var chatRooms = this.chatRepository.AllAsNoTracking()
               .To<T>().ToList();

            return chatRooms;
        }

        public IEnumerable<T> GetAllGeneralChatMessages<T>(int id = 1)
        {
            var messages = this.chatMessageRepository.AllAsNoTracking()
                .Where(x => x.ChatId == id)
                .To<T>().ToList();

            return messages;
        }

        public T GetById<T>(int id)
        {
            var chatRoom = this.chatRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
               .To<T>().FirstOrDefault();

            return chatRoom;
        }
    }
}

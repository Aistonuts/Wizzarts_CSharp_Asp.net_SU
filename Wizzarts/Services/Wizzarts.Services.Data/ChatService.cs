namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<T>> GetAllChatMessagesInChatRoom<T>(int id)
        {
            var messages = await this.chatMessageRepository.AllAsNoTracking()
                .Where(x => x.ChatId == id)
                .To<T>().ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<T>> GetAllChatRooms<T>()
        {
            var chatRooms = await this.chatRepository.AllAsNoTracking()
               .To<T>().ToListAsync();

            return chatRooms;
        }

        public async Task<IEnumerable<T>> GetAllGeneralChatMessages<T>(int id = 1)
        {
            var messages = await this.chatMessageRepository.AllAsNoTracking()
                .Where(x => x.ChatId == id)
                .To<T>().ToListAsync();

            return messages;
        }

        public async Task<T> GetById<T>(int id)
        {
            var chatRoom = await this.chatRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
               .To<T>().FirstOrDefaultAsync();

            return chatRoom;
        }
    }
}

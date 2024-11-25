namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChatService
    {
        Task<IEnumerable<T>> GetAllGeneralChatMessages<T>(int id);

        Task<IEnumerable<T>> GetAllChatMessagesInChatRoom<T>(int id);

        Task<IEnumerable<T>> GetAllChatRooms<T>();

        Task<T> GetById<T>(int id);
    }
}

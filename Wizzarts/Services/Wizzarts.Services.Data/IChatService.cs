namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;

    public interface IChatService
    {
        IEnumerable<T> GetAllGeneralChatMessages<T>(int id);

        IEnumerable<T> GetAllChatMessagesInChatRoom<T>(int id);

        IEnumerable<T> GetAllChatRooms<T>();

        T GetById<T>(int id);
    }
}

namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<T> GetById<T>(int id);

        Task<IEnumerable<T>> GetAllOrdersByUserId<T>(string id);

        Task<IEnumerable<T>> GetAll<T>();

        // Task OrderAsync(SingleExpansionViewModel input, string userId);
        Task PauseOrder(int id);

        Task ShipOrder(int id);

        Task ReadyOrder(int id);

        Task CancelOrder(int id);

        Task<IEnumerable<T>> GetAllCardsInOrderId<T>(int id);

        Task<bool> HasUserWithIdAsync(int orderId, string userId);
    }
}

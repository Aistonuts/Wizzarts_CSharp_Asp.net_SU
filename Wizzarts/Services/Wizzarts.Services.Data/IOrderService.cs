namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.CardGameExpansion;

    public interface IOrderService
    {
        Task<T> GetById<T>(int id);

        IEnumerable<T> GetAllOrdersByUserId<T>(string id);

        IEnumerable<T> GetAll<T>();

        Task OrderAsync(SingleExpansionViewModel input, string userId);

        Task PauseOrder(int id);

        Task ShipOrder(int id);

        Task ReadyOrder(int id);

        Task CancelOrder(int id);

        IEnumerable<T> GetAllCardsInOrderId<T>(int id);

        Task<bool> HasUserWithIdAsync(int orderId, string userId);
    }
}

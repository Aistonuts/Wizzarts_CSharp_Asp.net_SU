namespace Wizzarts.Web.ViewModels.Order
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class OrderStatusViewModel : IMapFrom<OrderStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}

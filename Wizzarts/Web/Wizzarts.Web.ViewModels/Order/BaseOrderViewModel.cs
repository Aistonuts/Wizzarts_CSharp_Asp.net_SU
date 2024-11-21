namespace Wizzarts.Web.ViewModels.Order
{
    using System;

    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class BaseOrderViewModel : IndexAuthenticationViewModel, IMapFrom<Order>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DeckId { get; set; }

        public string DeckImageUrl { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public DateTime? EstimatedDeliveryDate { get; set; }

        public int OrderStatusId { get; set; }
    }
}

namespace Wizzarts.Web.ViewModels.Store
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class StoreInListViewModel : IndexAuthenticationViewModel, IMapFrom<Store>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public string StoreOwnerId { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }
    }
}

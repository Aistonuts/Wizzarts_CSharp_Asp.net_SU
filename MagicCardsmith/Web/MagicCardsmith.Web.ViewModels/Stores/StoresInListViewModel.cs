namespace MagicCardsmith.Web.ViewModels.Stores
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class StoresInListViewModel : IMapFrom<Store>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string StoreOwnerId { get; set; }

        public bool ApprovedByAdmin { get; set; }
    }
}

namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class Store : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string StoreOwnerId { get; set; }

        public ApplicationUser StoreOwner { get; set; }
    }
}

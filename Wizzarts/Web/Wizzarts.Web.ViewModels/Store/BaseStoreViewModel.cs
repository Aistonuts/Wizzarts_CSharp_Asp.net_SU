namespace Wizzarts.Web.ViewModels.Store
{
    using Wizzarts.Web.ViewModels.Home;

    public class BaseStoreViewModel : IndexAuthenticationViewModel
    {
        public string StoreName { get; set; }

        public string StoreCountry { get; set; }

        public string StoreCity { get; set; }

        public string StorePhoneNumber { get; set; }

        public string StoreAddress { get; set; }

        public string StoreOwnerId { get; set; }
    }
}

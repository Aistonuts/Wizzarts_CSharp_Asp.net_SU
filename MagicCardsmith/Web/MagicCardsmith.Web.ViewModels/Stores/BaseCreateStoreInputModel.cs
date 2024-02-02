namespace MagicCardsmith.Web.ViewModels.Stores
{
    using Microsoft.AspNetCore.Http;

    public abstract class BaseCreateStoreInputModel
    {
        public string StoreName { get; set; }

        public string StoreCountry { get; set; }

        public string StoreCity { get; set; }

        public string StorePhoneNumber { get; set; }

        public string StoreAddress { get; set; }

        public IFormFile StoreImage { get; set; }

        public string StoreOwnerId { get; set; }
    }
}

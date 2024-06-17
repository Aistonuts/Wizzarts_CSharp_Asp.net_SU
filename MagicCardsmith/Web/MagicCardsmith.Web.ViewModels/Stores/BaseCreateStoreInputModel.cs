namespace MagicCardsmith.Web.ViewModels.Stores
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static MagicCardsmith.Common.DataConstants;

    public abstract class BaseCreateStoreInputModel
    {
        [Required(ErrorMessage = "Store name is required!")]
        [StringLength(StoreNameMaxLength, MinimumLength = StoreNameMinLength, ErrorMessage = "Store name  should be between 5 and 30 characters long")]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Store location is required!")]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = "Store location should be between 5 and 50 characters long")]
        [Display(Name = "Country")]
        public string StoreCountry { get; set; }

        [Required(ErrorMessage = "Store location is required!")]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = "Store location should be between 5 and 50 characters long")]
        [Display(Name = "City")]
        public string StoreCity { get; set; }

        [Required(ErrorMessage = "Store phone number is required!")]
        [StringLength(StorePhoneMaxLength, MinimumLength = StorePhoneMinLength, ErrorMessage = "Store phone number should be between 7 and 15 characters long")]
        [Display(Name = "Phone mumber")]
        public string StorePhoneNumber { get; set; }

        [Required(ErrorMessage = "Store location is required!")]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = "Store location should be between 5 and 50 characters long")]
        [Display(Name = "Address")]
        public string StoreAddress { get; set; }

        [Required(ErrorMessage = "Store image is required!")]
        public IFormFile StoreImage { get; set; }

        public string StoreOwnerId { get; set; }
    }
}

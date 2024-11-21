namespace Wizzarts.Web.ViewModels.Store
{
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class BaseStoreViewModel : IndexAuthenticationViewModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(StoreNameMaxLength, MinimumLength = StoreNameMinLength, ErrorMessage = LengthMessage)]
        public string StoreName { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = LengthMessage)]
        public string StoreCountry { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = LengthMessage)]
        public string StoreCity { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(StorePhoneMaxLength, MinimumLength = StorePhoneMinLength, ErrorMessage = LengthMessage)]
        public string StorePhoneNumber { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = LengthMessage)]
        public string StoreAddress { get; set; } = string.Empty;

        public string StoreOwnerId { get; set; } = string.Empty;
    }
}

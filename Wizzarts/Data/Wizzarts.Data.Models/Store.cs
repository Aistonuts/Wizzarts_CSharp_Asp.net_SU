namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class Store : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(StoreNameMaxLength)]
        [Comment("Store name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(StoreLocationMaxLength)]
        [Comment("Store location")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(StoreLocationMaxLength)]
        [Comment("Store location")]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(StorePhoneMaxLength)]
        [Comment("Store phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(StoreLocationMaxLength)]
        [Comment("Store location")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Comment("Store image Url")]
        public string Image { get; set; } = string.Empty;

        [Comment("Store approved by Admin.")]
        public bool ApprovedByAdmin { get; set; }

        [Required]
        public string StoreOwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(StoreOwnerId))]
        public ApplicationUser StoreOwner { get; set; }


    }
}

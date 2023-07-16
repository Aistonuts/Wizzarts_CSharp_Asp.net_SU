namespace MagicCardsHub.Data.Models
{
    using System;

    using MagicCardsHub.Data.Common.Models;

    public class Package : BaseDeletableModel<string>
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public string ShippingAddress { get; set; }

        public int StatusId { get; set; }

        public virtual PackageStatus Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public string RecipientId { get; set; }

        public virtual ApplicationUser Recipient { get; set; }
    }
}

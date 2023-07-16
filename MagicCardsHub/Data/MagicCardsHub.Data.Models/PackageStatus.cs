namespace MagicCardsHub.Data.Models
{
    using System;

    using MagicCardsHub.Data.Common.Models;

    public class PackageStatus : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}

namespace MagicCardsHub.Data.Models
{
    using MagicCardsHub.Data.Common.Models;

    public class VarSymbols : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }
    }
}

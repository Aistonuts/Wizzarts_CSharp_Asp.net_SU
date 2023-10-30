namespace MagicCardsHub.Web.ViewModels.CardSet
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateCardSetInputModel : BaseCreateCardSetInputModel
    {
        public IFormFile Image { get; set; }

        public IEnumerable<IFormFile> ManaSymbols { get; set; }

        public IEnumerable<IFormFile> VariousSymbols { get; set; }
    }
}

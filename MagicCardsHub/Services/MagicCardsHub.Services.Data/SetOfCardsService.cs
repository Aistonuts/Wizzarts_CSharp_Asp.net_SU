namespace MagicCardsHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Web.ViewModels.CardSet;

    using static System.Net.Mime.MediaTypeNames;

    public class SetOfCardsService 
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<ExpansionCardDeck> expansionCardDeckRepository;

        public SetOfCardsService(
            IDeletableEntityRepository<ExpansionCardDeck> expansionCardDeckRepository)
        {
            this.expansionCardDeckRepository = expansionCardDeckRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}

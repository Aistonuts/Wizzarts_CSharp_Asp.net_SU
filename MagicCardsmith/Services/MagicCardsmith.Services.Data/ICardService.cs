namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Card;

    public interface ICardService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        T GetById<T>(int id);

        int GetCount();

        IEnumerable<T> GetRandom<T>(int count);

        IEnumerable<T> GetAllByCardId<T>(int id);

        Task CreateAsync(CreateCardInputModel input, string userId, string id, string path);
    }
}

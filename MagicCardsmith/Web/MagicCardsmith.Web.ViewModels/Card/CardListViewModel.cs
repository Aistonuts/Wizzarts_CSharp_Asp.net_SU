namespace MagicCardsmith.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Artist;

    public class CardListViewModel : PagingViewModel
    {
        public IEnumerable<CardInListViewModel> Cards { get; set; }
    }
}

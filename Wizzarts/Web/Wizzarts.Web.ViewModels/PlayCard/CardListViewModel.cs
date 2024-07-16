namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System.Collections.Generic;
    using Wizzarts.Web.ViewModels.Home;

    public class CardListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<CardInListViewModel> Cards { get; set; }
    }
}

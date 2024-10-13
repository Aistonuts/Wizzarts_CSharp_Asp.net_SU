namespace Wizzarts.Web.ViewModels.Deck
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class DeckListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<DeckInListViewModel> Decks { get; set; }
    }
}

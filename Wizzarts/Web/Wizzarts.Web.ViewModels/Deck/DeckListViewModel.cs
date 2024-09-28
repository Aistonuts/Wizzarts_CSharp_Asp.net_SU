using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Home;

namespace Wizzarts.Web.ViewModels.Deck
{
    public class DeckListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<DeckInListViewModel> Decks { get; set; }
    }
}

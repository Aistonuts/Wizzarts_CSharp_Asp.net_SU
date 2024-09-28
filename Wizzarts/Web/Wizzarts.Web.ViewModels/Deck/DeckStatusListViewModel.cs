using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.Deck
{
    public class DeckStatusListViewModel : IMapFrom<DeckStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizzarts.Web.ViewModels.Search
{
    public class SearchAutocompleteViewModel
    {
        public string CardName { get; set; } = string.Empty;

        public string CombinedName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}

namespace MagicCardsmith.Web.ViewModels.SearchCard
{
    using System.Collections.Generic;

    public class SearchListInputModel
    {
        public IEnumerable<int> TypeOfCards { get; set; }

        public string CardName { get; set; }
    }
}

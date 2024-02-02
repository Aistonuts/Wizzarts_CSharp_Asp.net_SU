namespace MagicCardsmith.Services
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels.SearchCard;

    public class StatisticsServiceModel
    {
        public int TotalCards { get; set; }

        public int TotalArt { get; set; }

        public IEnumerable<string> Names { get; set; }
    }
}

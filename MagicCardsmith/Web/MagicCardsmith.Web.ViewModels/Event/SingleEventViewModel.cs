namespace MagicCardsmith.Web.ViewModels.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SingleEventViewModel
    {
        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string EventCreatorId { get; set; }

        public IEnumerable<MilestonesInListViewModel> Milestones { get; set; }
    }
}

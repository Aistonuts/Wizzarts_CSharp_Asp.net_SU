namespace Wizzarts.Web.Areas.Administration.Models.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EventCategoryInListViewModel : IMapFrom<EventCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

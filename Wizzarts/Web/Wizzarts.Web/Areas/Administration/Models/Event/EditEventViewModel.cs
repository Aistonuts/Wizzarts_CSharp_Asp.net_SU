namespace Wizzarts.Web.Areas.Administration.Models.Event
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EditEventViewModel : BaseEventViewModel, IMapFrom<Event>
    {
        public int Id { get; set; }
    }
}

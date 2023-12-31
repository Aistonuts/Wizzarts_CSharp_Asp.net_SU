namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class EventStatus : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}

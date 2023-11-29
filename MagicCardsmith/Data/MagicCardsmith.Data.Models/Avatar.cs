namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class Avatar : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }
    }
}

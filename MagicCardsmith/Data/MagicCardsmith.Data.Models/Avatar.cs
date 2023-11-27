using MagicCardsmith.Data.Common.Models;

namespace MagicCardsmith.Data.Models
{
    public class Avatar : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }
    }
}

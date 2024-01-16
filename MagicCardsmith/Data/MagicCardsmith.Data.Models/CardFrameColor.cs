namespace MagicCardsmith.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class CardFrameColor : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}

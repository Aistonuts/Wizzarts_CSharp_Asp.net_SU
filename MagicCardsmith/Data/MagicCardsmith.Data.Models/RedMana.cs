namespace MagicCardsmith.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class RedMana : BaseDeletableModel<int>
    {
        public int Cost { get; set; }

        public string ColorName { get; set; }

        public string ImageUrl { get; set; }
    }
}

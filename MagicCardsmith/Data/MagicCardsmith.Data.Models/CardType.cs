namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Common.Models;

    public class CardType : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}

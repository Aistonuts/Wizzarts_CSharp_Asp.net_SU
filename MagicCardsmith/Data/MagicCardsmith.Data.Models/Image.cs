namespace MagicCardsmith.Data.Models
{
    using System;

    using MagicCardsmith.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}

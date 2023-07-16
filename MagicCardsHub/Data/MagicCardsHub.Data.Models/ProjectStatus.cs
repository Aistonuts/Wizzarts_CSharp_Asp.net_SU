namespace MagicCardsHub.Data.Models
{
    using System;

    using MagicCardsHub.Data.Common.Models;

    public class ProjectStatus : BaseDeletableModel<string>
    {
        public ProjectStatus()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
    }
}

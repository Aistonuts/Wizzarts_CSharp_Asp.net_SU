namespace MagicCardsmith.Data.Models
{
    using System.Collections.Generic;

    public class Artist
    {
        public Artist()
        {
            this.Art = new HashSet<Art>();
        }

        public virtual ICollection<Art> Art { get; set; }
    }
}

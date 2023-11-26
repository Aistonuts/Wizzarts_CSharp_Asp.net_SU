namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public string ArtId { get; set; }

        public virtual Art Art { get; set; }

        public int CardId { get; set; }

        public Card Card { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}

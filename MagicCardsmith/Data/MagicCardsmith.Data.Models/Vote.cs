namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}

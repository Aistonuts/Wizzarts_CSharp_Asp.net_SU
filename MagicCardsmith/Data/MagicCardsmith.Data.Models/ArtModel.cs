namespace MagicCardsmith.Data.Models
{
    public class ArtModel : Image
    {
        public int ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }
    }
}

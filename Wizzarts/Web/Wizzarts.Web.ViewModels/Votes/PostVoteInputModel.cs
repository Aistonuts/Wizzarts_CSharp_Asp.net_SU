using System.ComponentModel.DataAnnotations;

namespace Wizzarts.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        public string CardId { get; set; } = string.Empty;

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}

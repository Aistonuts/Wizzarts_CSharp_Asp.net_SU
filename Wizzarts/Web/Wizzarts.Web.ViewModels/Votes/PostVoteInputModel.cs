using System.ComponentModel.DataAnnotations;

namespace Wizzarts.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        public string CardId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}

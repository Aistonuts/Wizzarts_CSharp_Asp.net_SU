namespace MagicCardsmith.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVoteService votesService;

        public VotesController(IVoteService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.CardId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVotes(input.CardId);
            return new PostVoteResponseModel { AverageVote = averageVotes };
        }
    }
}

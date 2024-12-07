namespace Wizzarts.Services.Data.Tests.VoteServiceTest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Xunit;

    public class VoteServiceTest : UnitTestBase
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnly1VoteShouldBeCounted()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback(
                (Vote vote) => list.Add(vote));
            var service = new VoteService(mockRepo.Object);

            await service.SetVoteAsync("c330fecf-61a9-4e03-8052-cd2b9583a251", "1", 1);
            await service.SetVoteAsync("c330fecf-61a9-4e03-8052-cd2b9583a251", "1", 5);
            await service.SetVoteAsync("c330fecf-61a9-4e03-8052-cd2b9583a251", "1", 5);
            await service.SetVoteAsync("c330fecf-61a9-4e03-8052-cd2b9583a251", "1", 5);
            await service.SetVoteAsync("c330fecf-61a9-4e03-8052-cd2b9583a251", "1", 5);

            Assert.Equal(1, list.Count);
            Assert.Equal(5, list.First().Value);
        }

        [Fact]
        public async Task When2UsersVoteForTheSameRecipeTheAverageVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback(
                (Vote vote) => list.Add(vote));
            var service = new VoteService(mockRepo.Object);

            await service.SetVoteAsync("f43639ef-5503-4e8a-a75d-5651c645a03d", "Niki", 5);
            await service.SetVoteAsync("f43639ef-5503-4e8a-a75d-5651c645a03d", "Pesho", 1);
            await service.SetVoteAsync("f43639ef-5503-4e8a-a75d-5651c645a03d", "Niki", 2);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Exactly(2));

            Assert.Equal(2, list.Count);
            Assert.Equal(1.5, service.GetAverageVotes("f43639ef-5503-4e8a-a75d-5651c645a03d"));
        }
    }
}

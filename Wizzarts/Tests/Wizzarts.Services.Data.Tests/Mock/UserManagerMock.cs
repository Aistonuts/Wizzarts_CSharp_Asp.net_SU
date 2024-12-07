namespace Wizzarts.Services.Data.Tests.Mock
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Wizzarts.Data.Models;

    public class UserManagerMock
    {
        public static Mock<UserManager<ApplicationUser>> New
            => new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
    }
}

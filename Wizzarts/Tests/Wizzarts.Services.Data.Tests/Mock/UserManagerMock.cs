namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

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

using Microsoft.AspNetCore.Identity;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Wizzarts.Data.Models;

namespace Wizzarts.Services.Data.Tests.Mock
{
    public class RoleManagerMock
    {
        public static Mock<RoleManager<IdentityRole>> New
            => new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object);
    }
}

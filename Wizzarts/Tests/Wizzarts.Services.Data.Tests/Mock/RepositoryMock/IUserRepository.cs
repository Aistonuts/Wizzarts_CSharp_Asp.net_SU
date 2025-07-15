using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;

namespace Wizzarts.Services.Data.Tests.Mock.RepositoryMock
{
    public interface IUserRepository : IDisposable
    {
        Task<List<ApplicationUser>> ToListAsync();
        Task<ApplicationUser> FindByIDAsync(string userId);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task AddToRoleAsync(ApplicationUser user, string roleName);
        Task RemoveFromRoleAsync(ApplicationUser user, string roleName);
        Task<bool> AnyAsync(string userId);
        Task AddAsync(ApplicationUser user);
        Task DeleteAsync(string userId);
        void Update(ApplicationUser user);
        Task SaveChangesAsync();
    }
}

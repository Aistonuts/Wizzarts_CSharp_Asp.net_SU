using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Data;

namespace Wizzarts.Services.Data.Tests.Mock.RepositoryMock
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<ApplicationUser> FindByIDAsync(string userId)
        {
            return _context.Users.FirstOrDefaultAsync(x=>x.Id == userId);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return _context.Users.SingleOrDefaultAsync(m => m.UserName == userName);
        }

        public Task<List<ApplicationUser>> ToListAsync()
        {
            return _context.Users.ToListAsync();
        }

        public Task AddAsync(ApplicationUser user)
        {
            _context.Users.AddAsync(user);
            return _context.SaveChangesAsync();
        }

        public void Update(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public Task DeleteAsync(string userId)
        {
            ApplicationUser user = _context.Users.Find(userId);
            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            _userManager.AddToRoleAsync(user, roleName);
            return _context.SaveChangesAsync();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            _userManager.RemoveFromRoleAsync(user, roleName);
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<bool> AnyAsync(string userId)
        {
            return _context.Users.AnyAsync(e => e.Id == userId);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

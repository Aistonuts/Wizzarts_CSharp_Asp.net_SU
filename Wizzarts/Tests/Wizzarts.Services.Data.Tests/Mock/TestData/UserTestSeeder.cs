namespace WWizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Wizzarts.Data;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data.Tests.Mock;

    public class UserTestSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var hasher = new PasswordHasher<ApplicationUser>();
            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "2738e787-5d57-4bc7-b0d2-287242f04695",
                CreatedOn = DateTime.UtcNow,
                UserName = "Drawgoon",
                Nickname = "Drawgoon",
                AvatarUrl = "/images/avatar/Portrait_Drawgoon.png",
                NormalizedUserName = "DRAWGOON",
                Email = "drawgoon@aol.com",
                NormalizedEmail = "DRAWGOO@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd1"),
                Role = new ApplicationRole() { Name = "Admin" },
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "0ac1e577-c7ff-4aa3-83c3-e5acac9de281",
                CreatedOn = DateTime.UtcNow,
                UserName = "Glowei",
                AvatarUrl = "/images/avatar/Portrait_Glowei.png",
                NormalizedUserName = "GLOWEI",
                Email = "glowei@aol.com",
                NormalizedEmail = "GLOWEI@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd2"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                CreatedOn = DateTime.UtcNow,
                UserName = "Metzen",
                AvatarUrl = "/images/avatar/Portrait_Metzen.png",
                NormalizedUserName = "METZEN",
                Email = "metzen@aol.com",
                NormalizedEmail = "METZEN@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd3"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "f6f94be8-49e0-4a28-9e7f-797c40e7e169",
                CreatedOn = DateTime.UtcNow,
                UserName = "MrJack",
                AvatarUrl = "/images/avatar/Portrait_MrJack.png",
                NormalizedUserName = "MRJACK",
                Email = "mrjack@aol.com",
                NormalizedEmail = "MRJACK@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd4"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "b4accad4-e878-4de3-a317-665d0a43fbd3",
                CreatedOn = DateTime.UtcNow,
                UserName = "Raneman",
                AvatarUrl = "/images/avatar/Portrait_Raneman.png",
                NormalizedUserName = "RANEMAN",
                Email = "raneman@aol.com",
                NormalizedEmail = "RANEMAN@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd5"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "5823bbf1-993c-416b-9bf1-c358fedf38a6",
                CreatedOn = DateTime.UtcNow,
                UserName = "RedKnuckle",
                AvatarUrl = "/images/avatar/Portrait_RedKnuckle.png",
                NormalizedUserName = "REDKNUCKLE",
                Email = "redknuckle@aol.com",
                NormalizedEmail = "REDKNUCKLE@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd5"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "ad8dada2-c947-4ad3-aaa1-e530f13d21c1",
                CreatedOn = DateTime.UtcNow,
                UserName = "Samwise",
                AvatarUrl = "/images/avatar/Portrait_Samwise.png",
                NormalizedUserName = "SAMWISE",
                Email = "samwise@aol.com",
                NormalizedEmail = "SAMWISE@AOL.COM",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd6"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "799d728e-0c16-4e4a-81b3-48a113a88cf1",
                CreatedOn = DateTime.UtcNow,
                UserName = "Thammer",
                AvatarUrl = "/images/avatar/Portrait_Thammer.png",
                NormalizedUserName = "THAMMER",
                Email = "thammer@aol.com",
                NormalizedEmail = "THAMMER",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd7"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "eb49ba9d-5030-42b6-8aef-c93506943fde",
                CreatedOn = DateTime.UtcNow,
                UserName = "Twincruiser",
                AvatarUrl = "/images/avatar/Portrait_Twincruiser.png",
                NormalizedUserName = "TWINCRUISER",
                Email = "twincruiser@aol.com",
                NormalizedEmail = "TWINCRUISER",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd8"),
            });

            await dbContext.Users.AddAsync(new ApplicationUser
            {
                Id = "4a50fd21-682d-42b8-9b30-168d120ad224",
                CreatedOn = DateTime.UtcNow,
                UserName = "Test",
                AvatarUrl = "/images/avatar/Portrait_Twincruiser.png",
                NormalizedUserName = "test",
                Email = "test@aol.com",
                NormalizedEmail = "TEST",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd9"),
            });
            await dbContext.SaveChangesAsync();
        }
    }
}

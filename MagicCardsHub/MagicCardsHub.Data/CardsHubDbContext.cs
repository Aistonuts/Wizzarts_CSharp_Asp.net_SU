

using MagicCardsHub.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCardsHub.Data
{
    public class CardsHubDbContext : IdentityDbContext<ProjectCreator, CreatorRole, string>
    {
        public DbSet<Art> Arts { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        public DbSet<GameSeason> GameSeasons { get; set; }

        public CardsHubDbContext(DbContextOptions<CardsHubDbContext> options) : base(options)
        {
        }

        public CardsHubDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PJE8JOP\SQLEXPRESS;Database=MagicCardsHubDb;TrustServerCertificate=true;Trusted_Connection=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectCreator>()
                .HasKey(user => user.Id);

            builder
                .Entity<Art>()
                .HasOne(c => c.Artist)
                .WithMany(c => c.ArtPieces)
                .HasForeignKey(c => c.ArtIstId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Card>()
                .HasOne(c => c.GameSeason)
                .WithMany(d => d.Cards)
                .HasForeignKey(c => c.GameSeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<GameSeason>()
                .HasOne<ProjectCreator>()
                .WithMany(d => d.GameSeasons)
                .HasForeignKey(c => c.ProjectCreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Art>()
                .HasOne(art => art.Card)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}




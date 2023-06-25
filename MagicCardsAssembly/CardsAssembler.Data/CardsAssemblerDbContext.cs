
using CardsAssembler.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace CardsAssembler.Data
{
    public class CardsAssemblerDbContext : IdentityDbContext<CardCreator, CreatorRole, string>
    {
        public DbSet<Art> Arts { get; set; }

        public DbSet<CardStats> CardStats { get; set; }

        public DbSet<CardStatus> CardStatuses { get; set; }

        public DbSet<Card> GameCards { get; set; }

        public DbSet<CardAssembly> GameProjectCards { get; set; }
        public CardsAssemblerDbContext(DbContextOptions<CardsAssemblerDbContext> options) : base(options)
        {
        }



        public CardsAssemblerDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PJE8JOP\SQLEXPRESS;Database=CardAssemblerDB;TrustServerCertificate=true;Trusted_Connection=true;");

            base.OnConfiguring(optionsBuilder);
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CardCreator>()
                    .HasKey(user => user.Id);
                    

            builder.Entity<CardCreator>()
                      .HasMany(artist => artist.CardsArt)
                      .WithOne(art => art.Artist)
                      .HasForeignKey(art => art.ArtIstId)
                      .OnDelete(DeleteBehavior.Restrict);
                     

            builder.Entity<CardCreator>()
                     .HasMany(project => project.Cards)
                     .WithOne(card => card.ProjectManager)
                     .HasForeignKey(card => card.ProjectManagerId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CardAssembly>()
                     .HasMany(card => card.GameCard)
                     .WithOne(project => project.Project)
                     .HasForeignKey(project => project.ProjectId)
                     .OnDelete(DeleteBehavior.Restrict);


           builder.Entity<Card>()
                  .HasOne(art => art.Art)
                  .WithOne()
                  .OnDelete(DeleteBehavior.Restrict);
       

            base.OnModelCreating(builder);


        }
    }
}

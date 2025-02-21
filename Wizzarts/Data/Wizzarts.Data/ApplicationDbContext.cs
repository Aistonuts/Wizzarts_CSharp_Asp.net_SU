﻿namespace Wizzarts.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;
    using Wizzarts.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Art> Arts { get; set; }

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<WizzartsTeam> WizzartsTeamMembers { get; set; }

        public DbSet<CardGameExpansion> CardGameExpansions { get; set; }

        public DbSet<PlayCard> PlayCards { get; set; }

        public DbSet<CommentCard> CardComments { get; set; }

        public DbSet<PlayCardFrameColor> PlayCardFrameColors { get; set; }

        public DbSet<PlayCardType> PlayCardTypes { get; set; }

        public DbSet<EventStatus> EventStatuses { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventCategory> EventCategories { get; set; }

        public DbSet<CardDeck> CardDecks { get; set; }

        public DbSet<DeckOfCards> DeckOfCards { get; set; }

        public DbSet<DeckStatus> DeckStatuses { get; set; }

        public DbSet<EventComponent> EventComponents { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<ManaInCard> ManaInCard { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<WizzartsGameRules> WizzartsGameRules { get; set; }

        public DbSet<WizzartsGameRulesData> WizzartsGameRulesData { get; set; }

        public DbSet<WizzartsCardGame> WizzartsCardGame { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CardOrder> CardOrders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<TagHelpAction> TagHelpActions { get; set; }

        public DbSet<TagHelpController> TagHelpControllers { get; set; }

        public DbSet<ManaCost> ManaCosts { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<CardOrder>()
            //.HasKey(c => new { c.OrderId, c.PlayCardId });

            builder.Entity<Order>()
                .HasMany(x => x.CardsInOrder)
                .WithMany(x => x.CardsInOrder)
                .UsingEntity<CardOrder>();

            builder.Entity<CardDeck>()
            .HasMany(s => s.PlayCards)
            .WithMany(c => c.DeckOfCards)
            .UsingEntity<DeckOfCards>();

            builder.Entity<ChatUser>()
            .HasKey(c => new { c.UserId, c.ChatId });

            builder.Entity<PlayCard>()
            .HasOne(a => a.Art)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EventStatus>()
            .HasMany(a => a.Events)
            .WithOne(a => a.Status)
            .HasForeignKey(a => a.EventStatusId);

            builder.Entity<EventCategory>()
            .HasMany(a => a.Events)
            .WithOne(a => a.EventCategory)
            .HasForeignKey(a => a.EventCategoryId);

            builder.Entity<DeckStatus>()
            .HasMany(a => a.Decks)
            .WithOne(a => a.Status)
            .HasForeignKey(a => a.StatusId);

            builder.Entity<WizzartsCardGame>()
            .HasOne(a => a.CardGameRules)
            .WithOne(a => a.WizzartsCardGame)
            .HasForeignKey<WizzartsGameRules>(a => a.WizzartsCardGameId);

            builder.Entity<WizzartsCardGame>()
            .HasMany(a => a.GameRulesData)
            .WithOne(a => a.WizzartsCardGame)
            .HasForeignKey(a => a.WizzartsCardGameId);

            builder.Entity<WizzartsCardGame>()
            .HasMany(a => a.WizzartsTeamMembers)
            .WithOne(a => a.WizzartsCardGame)
            .HasForeignKey(a => a.WizzartsCardGameId);

            builder.Entity<OrderStatus>()
            .HasMany(a => a.DeckOrders)
            .WithOne(a => a.OrderStatus)
            .HasForeignKey(a => a.OrderStatusId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Articles)
            .WithOne(a => a.ArticleCreator)
            .HasForeignKey(a => a.ArticleCreatorId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Orders)
            .WithOne(a => a.Recipient)
            .HasForeignKey(a => a.RecipientId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Cards)
            .WithOne(a => a.AddedByMember)
            .HasForeignKey(a => a.AddedByMemberId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Events)
            .WithOne(a => a.EventCreator)
            .HasForeignKey(a => a.EventCreatorId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Stores)
            .WithOne(a => a.StoreOwner)
            .HasForeignKey(a => a.StoreOwnerId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Votes)
            .WithOne(a => a.AddedByMember)
            .HasForeignKey(a => a.AddedByMemberId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Art)
            .WithOne(a => a.AddedByMember)
            .HasForeignKey(a => a.AddedByMemberId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.CardDecks)
            .WithOne(a => a.CreatedByMember)
            .HasForeignKey(a => a.CreatedByMemberId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.Comments)
            .WithOne(a => a.PostedByUser)
            .HasForeignKey(a => a.PostedByUserId);

            builder.Entity<Avatar>()
            .HasMany(a => a.Members)
            .WithOne(a => a.Avatar)
            .HasForeignKey(a => a.AvatarId);

            builder.Entity<PlayCardFrameColor>()
            .HasMany(a => a.Cards)
            .WithOne(a => a.CardFrameColor)
            .HasForeignKey(a => a.CardFrameColorId);

            builder.Entity<PlayCardType>()
            .HasMany(a => a.Cards)
            .WithOne(a => a.CardType)
            .HasForeignKey(a => a.CardTypeId);

            builder.Entity<CardGameExpansion>()
            .HasMany(a => a.Cards)
            .WithOne(a => a.CardGameExpansion)
            .HasForeignKey(a => a.CardGameExpansionId);

            builder.Entity<ApplicationUser>()
            .HasMany(a => a.CardDecks)
            .WithOne(a => a.CreatedByMember)
            .HasForeignKey(a => a.CreatedByMemberId);

            builder.Entity<PlayCard>()
            .HasMany(a => a.CardMana)
            .WithOne(a => a.Card)
            .HasForeignKey(a => a.CardId);

            builder.Entity<PlayCard>()
            .HasMany(a => a.Comments)
            .WithOne(a => a.Card)
            .HasForeignKey(a => a.CardId);

            builder.Entity<PlayCard>()
            .HasMany(a => a.Votes)
            .WithOne(a => a.Card)
            .HasForeignKey(a => a.CardId);

            builder.Entity<TagHelpAction>()
            .HasMany(a => a.Events)
            .WithOne(a => a.ActionName)
            .HasForeignKey(a => a.ActionId);

            builder.Entity<TagHelpController>()
            .HasMany(a => a.Events)
            .WithOne(a => a.ControllerName)
            .HasForeignKey(a => a.ControllerId);

            builder.Entity<TagHelpAction>()
            .HasMany(a => a.EventComponents)
            .WithOne(a => a.ActionName)
            .HasForeignKey(a => a.ActionId);

            builder.Entity<TagHelpController>()
            .HasMany(a => a.EventComponents)
            .WithOne(a => a.ControllerName)
            .HasForeignKey(a => a.ControllerId);

            builder.Entity<Event>()
            .HasMany(a => a.EventComponents)
            .WithOne(a => a.Event)
            .HasForeignKey(a => a.EventId);

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}

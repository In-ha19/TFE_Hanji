using Gestionnaire_Collections.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire_Collections.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category_Article> Category_Articles { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Family_User> Family_Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notepad> Notepads { get; set; }
        public DbSet<MemberShip_Family> MemberShip_Families { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  

            // Configuration de la relation many-to-many entre Family et ApplicationUser via Family_User
            modelBuilder.Entity<Family_User>()
                .HasKey(fu => new { fu.FamilyId, fu.UserId });

            modelBuilder.Entity<Family_User>()
                .HasOne(fu => fu.Family)
                .WithMany(f => f.Family_Users)
                .HasForeignKey(fu => fu.FamilyId);

            modelBuilder.Entity<Family_User>()
                .HasOne(fu => fu.User)
                .WithMany(u => u.Family_Users)
                .HasForeignKey(fu => fu.UserId);

            // Configuration de la relation 1-N entre Category et ses SubCategories (auto-référentielle)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.Parent_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la relation many-to-many entre Category et Article via Category_Article
            modelBuilder.Entity<Category_Article>()
                .HasKey(ca => new { ca.CategoryId, ca.ArticleId });

            modelBuilder.Entity<Category_Article>()
                .HasOne(ca => ca.Category)
                .WithMany(c => c.Category_Articles)
                .HasForeignKey(ca => ca.CategoryId);

            modelBuilder.Entity<Category_Article>()
                .HasOne(ca => ca.Article)
                .WithMany(a => a.Category_Articles)
                .HasForeignKey(ca => ca.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation many-to-many entre ApplicationUser et Article via Collection
            modelBuilder.Entity<Collection>()
                .HasKey(c => new { c.UserId, c.ArticleId });

            modelBuilder.Entity<Collection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Collection>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Collections)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation 1-N entre ApplicationUser et Notepad
            modelBuilder.Entity<Notepad>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notepads)
                .HasForeignKey(n => n.UserId);

            // Configuration de la relation 1-N entre ApplicationUser et Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

            // Configuration de l'unicité pour ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.UserName)
            .IsUnique();

            // Configuration de l'unicité pour Article
            modelBuilder.Entity<Article>()
            .HasIndex(u => u.Name)
            .IsUnique();

            modelBuilder.Entity<Article>()
            .HasIndex(u => u.Isbn)
            .IsUnique();

            // Configuration de l'unicité pour Image
            modelBuilder.Entity<Image>()
            .HasIndex(u => u.ArticleId)
            .IsUnique();

            // Configuration de l'unicité pour Famille
            modelBuilder.Entity<Family>()
            .HasIndex(u => u.Name)
            .IsUnique();

            // Configuration de la relation 1-N entre ApplicationUser et MemberShip_Family

            modelBuilder.Entity<MemberShip_Family>()
           .HasOne(mf => mf.Requester)
           .WithMany()
           .HasForeignKey(mf => mf.RequesterId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberShip_Family>()
            .HasOne(mf => mf.Family)
            .WithMany()
            .HasForeignKey(mf => mf.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


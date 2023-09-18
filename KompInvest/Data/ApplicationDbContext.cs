using KompInvest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KompInvest.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        // Fluent API configurations
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // Important: This line has to be first.

            // User to Blog: One-to-Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Blogs)
                        .WithOne(b => b.User)
                        .HasForeignKey(b => b.UserId);

            // User to Comment: One-to-Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Comments)
                        .WithOne(c => c.Commenter)
                        .HasForeignKey(c => c.CommenterUserId);

            // Blog to Comment: One-to-Many
            modelBuilder.Entity<Blog>()
                        .HasMany(b => b.Comments)
                        .WithOne(c => c.Blog)
                        .HasForeignKey(c => c.BlogId);

            // User to Event: One-to-Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Events)
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId);

            // User to ForumPost: One-to-Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.ForumPosts)
                        .WithOne(fp => fp.Author)
                        .HasForeignKey(fp => fp.AuthorUserId);

            // User to Investment: One-to-Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Investments)
                        .WithOne(i => i.User)
                        .HasForeignKey(i => i.UserId);

            // User to Resource: One-to-Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Resources)
                        .WithOne(r => r.User)
                        .HasForeignKey(r => r.UserId);

            // User to UserProfile: One-to-One
            modelBuilder.Entity<User>()
                        .HasOne(u => u.UserProfile)
                        .WithOne(up => up.User)
                        .HasForeignKey<UserProfile>(up => up.UserId);
            // Set precision for decimal type
            modelBuilder.Entity<Investment>()
                        .Property(p => p.Amount)
                        .HasPrecision(18, 2);

            // Update delete behavior for Comment -> Blog relation
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}


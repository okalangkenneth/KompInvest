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

        // Define DbSets for each model
        public DbSet<User> Users { get; set; }
        public DbSet<MemberProfile> MemberProfiles { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships and any additional configurations

            // MemberProfile to User relationship
            modelBuilder.Entity<MemberProfile>()
                .HasOne(mp => mp.User)
                .WithOne(u => u.MemberProfile)
                .HasForeignKey<MemberProfile>(mp => mp.MemberID);

            // Investment to MemberProfile relationship
            modelBuilder.Entity<Investment>()
                .HasOne(i => i.MemberProfile)
                .WithMany(mp => mp.Investments)
                .HasForeignKey(i => i.MemberID);

            // Transaction relationships
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Investment)
                .WithMany(i => i.Transactions)
                .HasForeignKey(t => t.InvestmentID);

            // Testimonial to MemberProfile relationship
            modelBuilder.Entity<Testimonial>()
                .HasOne(t => t.MemberProfile)
                .WithMany(mp => mp.Testimonials)
                .HasForeignKey(t => t.MemberID);

            // Additional configurations as needed

            // Unique Constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Cascade Delete
            modelBuilder.Entity<MemberProfile>()
                .HasOne(mp => mp.User)
                .WithOne(u => u.MemberProfile)
                .OnDelete(DeleteBehavior.Cascade);

            // Specify Table Names
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<MemberProfile>().ToTable("MemberProfiles");
            modelBuilder.Entity<Investment>().ToTable("Investments");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<NewsArticle>().ToTable("NewsArticles");
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<Testimonial>().ToTable("Testimonials");

            // Configure Indices
            modelBuilder.Entity<Investment>()
                .HasIndex(i => new { i.MemberID, i.DateInvested });

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.TransactionDate);

            modelBuilder.Entity<NewsArticle>()
                .HasIndex(na => na.PublicationDate);
        }
    }
}



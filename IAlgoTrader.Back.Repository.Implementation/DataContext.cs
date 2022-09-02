using IAlgoTrader.Back.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAlgoTrader.Back.Repository.Implementation
{
    public class DataContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<ContactUsForm> ContactUsForms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.Metadata.RemoveIndex(new[] { builder.Property(u => u.NormalizedUserName).Metadata });
                builder.Metadata.RemoveIndex(new[] { builder.Property(u => u.UserName).Metadata });

                var index = modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName }).Metadata;
                modelBuilder.Entity<User>().Metadata.RemoveIndex(index);

                var index2 = modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName }).Metadata;
                modelBuilder.Entity<User>().Metadata.RemoveIndex(index2.Properties);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
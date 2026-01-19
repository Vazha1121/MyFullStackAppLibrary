using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries{ get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthorBook> AuthorProduct { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductType)
                .HasConversion<string>();
        }
    }
}

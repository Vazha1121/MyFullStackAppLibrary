using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Configuration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.publisherId);

            builder.ToTable("PublisherTB");

            builder.HasMany(p => p.Products)
                .WithOne(p => p.Publisher)
                .HasForeignKey(p => p.publisherId);
        }
    }
}

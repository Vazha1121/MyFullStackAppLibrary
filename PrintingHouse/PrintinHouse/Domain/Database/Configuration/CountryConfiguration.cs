using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.countryId);

            builder.ToTable("CounrtyTB");

            builder.HasMany(c => c.City)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.cityId);

        }
    }
}

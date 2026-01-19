using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Configuration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.cityId);

            builder.ToTable("CityTB");

            builder.HasOne(c => c.Country)
                .WithMany(c => c.City)
                .HasForeignKey(c => c.cityId);
        } 
    }
}

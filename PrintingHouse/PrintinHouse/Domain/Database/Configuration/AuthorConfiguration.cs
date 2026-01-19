using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.authorId);

            builder.ToTable(nameof(Author));


            builder.HasOne(c => c.Country)
                .WithMany(a => a.Authors)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
                
                
        }
    }
}

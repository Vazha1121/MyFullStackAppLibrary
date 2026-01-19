using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Configuration
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.HasKey(a => new { a.AuthorId, a.BookId });

            builder.HasOne(a => a.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(a => a.AuthorId);

            builder.HasOne(a => a.Book)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(a => a.BookId);
        }
    }
}

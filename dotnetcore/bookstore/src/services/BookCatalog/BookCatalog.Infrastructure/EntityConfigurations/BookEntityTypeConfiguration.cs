using BookCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Infrastructure.EntityConfigurations
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("book_hilo")
               .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(cb => cb.Description)
                .IsRequired(false)
                .HasMaxLength(400);

            builder.Property(cb => cb.Price)
                .IsRequired(true);

            builder.Property(cb => cb.ISBN)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(cb => cb.PublishedDate)
                .IsRequired(false);

            builder.HasOne(ci => ci.Category)
                .WithMany()
                .HasForeignKey(ci => ci.CategoryId);

            builder.HasOne(ci => ci.Author)
                .WithMany()
                .HasForeignKey(ci => ci.AuthorId);
        }
    }
}

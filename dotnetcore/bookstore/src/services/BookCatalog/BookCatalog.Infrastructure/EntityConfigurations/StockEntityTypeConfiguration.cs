using BookCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Infrastructure.EntityConfigurations
{
    internal class StockEntityTypeConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stock");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("stock_hilo")
               .IsRequired();

            builder.Property(cb => cb.Quantity)
                .IsRequired();

            builder.HasOne(ci => ci.Book)
                .WithMany()
                .HasForeignKey(ci => ci.BookId);
        }
    }
}

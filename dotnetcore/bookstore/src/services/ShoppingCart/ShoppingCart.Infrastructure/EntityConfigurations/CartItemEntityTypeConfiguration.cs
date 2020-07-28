using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Infrastructure.EntityConfigurations
{
    internal class CartItemEntityTypeConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItem");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("cartitem_hilo")
               .IsRequired();

            builder.Property(cb => cb.BookId)
                .IsRequired();

            builder.HasOne(ci => ci.Cart)
                .WithMany()
                .HasForeignKey(ci => ci.CartId);

            builder.Property(cb => cb.Price)
                .IsRequired(true);

            builder.Property(cb => cb.Quantity)
                .IsRequired(true);
        }    
    }
}

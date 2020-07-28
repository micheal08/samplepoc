using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Infrastructure.EntityConfigurations
{
    internal class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("cart_hilo")
               .IsRequired();

            builder.Property(cb => cb.Guid)
                .IsRequired();
        }
    }
}

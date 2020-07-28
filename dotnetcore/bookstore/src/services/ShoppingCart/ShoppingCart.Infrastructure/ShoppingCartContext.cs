using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.EntityConfigurations;
using System;

namespace ShoppingCart.Infrastructure
{
    public class ShoppingCartContext : DbContext
    {
        internal DbSet<Cart> Carts { get; set; }
        internal DbSet<CartItem> CartItems { get; set; }

        private readonly ILoggerFactory _loggerFactory;
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options,
            ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemEntityTypeConfiguration());
        }
    }
}

using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace BookCatalog.Infrastructure
{
    public class BookCatalogContext : DbContext
    {
        internal DbSet<Book> Books { get; set; }
        internal DbSet<Author> Authors { get; set; }
        internal DbSet<Category> Categories { get; set; }
        internal DbSet<Stock> Stocks { get; set; }

        private readonly ILoggerFactory _loggerFactory;
        public BookCatalogContext(DbContextOptions<BookCatalogContext> options, 
            ILoggerFactory loggerFactory) 
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StockEntityTypeConfiguration());
        }
    }
}

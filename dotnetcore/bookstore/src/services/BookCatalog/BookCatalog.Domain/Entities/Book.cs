using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Author AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}

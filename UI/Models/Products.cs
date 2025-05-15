using System;

namespace UI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public string Manufacturer { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductImageUrl { get; set; } // URL or base64
    }
}

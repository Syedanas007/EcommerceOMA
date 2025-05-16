using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string ProductCategoryName { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string ProductImage { get; set; } // This holds the Cloudinary URL

        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; } // Used only for the frontend upload
    }
}

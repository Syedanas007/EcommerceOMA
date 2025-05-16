using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Models
{
   public class Product
{
    public Guid Id { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    public string ProductCategoryName { get; set; }

    [Required]
    public string Manufacturer { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    // This stores the Cloudinary image URL
    [Required]
    public required string ProductImage { get; set; }
}

}


using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage =
            "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage =
            "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0", "99999999.99",
            ErrorMessage = "Price must be zero or greater.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue,
            ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}

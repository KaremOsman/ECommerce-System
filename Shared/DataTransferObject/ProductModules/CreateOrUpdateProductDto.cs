using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.ProductModules
{
    public class CreateOrUpdateProductDto
    {
        // Id is used for updates; it can be null during product creation
        public int? Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Product description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        // PictureUrl is used to display the existing image during updates
        public string? PictureUrl { get; set; }
        // Used to receive the actual image file from the Dashboard
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Brand selection is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Type selection is required.")]
        public int TypeId { get; set; }
    }
}
using Domain.Entities.ProductModule;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }

        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100,000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Type is Required")]
        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        [Required(ErrorMessage = "Product Brand is Required")]
        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }

    }
}

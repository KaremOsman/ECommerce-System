using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.ProductModules
{
    public class BrandDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand name is required.")]
        public string Name { get; set; } = null!;
    }
}

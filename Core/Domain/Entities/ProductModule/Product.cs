namespace Domain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        // 1-m between Product & ProductBrand
        public int BrandId { get; set; }
        public ProductBrand? Brand { get; set; }
        // 1-m between Product & ProductType
        public int TypeId { get; set; }
        public ProductType? Type { get; set; }

    }
}

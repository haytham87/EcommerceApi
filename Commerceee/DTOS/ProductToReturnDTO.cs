using Commerceee.DTOS;

namespace Commerceee.DTOS
{
    public class ProductToReturnDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
        public  string BrandType { get; set; }

        public  string ProductType { get; set; }
    }
}

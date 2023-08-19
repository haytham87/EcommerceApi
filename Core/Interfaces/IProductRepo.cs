using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepo
    {
        Task<IReadOnlyList<Product>> GetProductsList(int? brandId);
       Task<Product> GetProduct(int id);
        Task<IReadOnlyList<BrandType>> GetBrandsList();
        Task<IReadOnlyList<ProductType>> GetTypesList();
    }
}

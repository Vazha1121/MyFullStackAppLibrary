using PrintingHouse.Domain.Database.Repositories.DTOs.ProductDTO;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<Product> GetProductByID(int productId);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductByType(ProductType productType);
        Task<List<Product>> GetProductByAuthor(int authorId);   
        Task<int> CreateProduct(CreateProductDTO createProductDTO);
        Task<bool> UpdateProduct(UpdateProductDTO updateProductDTO,int productId);
        Task<bool> DeleteProduct(int productId);   
    }
}

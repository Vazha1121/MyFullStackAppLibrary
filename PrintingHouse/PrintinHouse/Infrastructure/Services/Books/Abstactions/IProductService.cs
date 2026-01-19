using PrintingHouse.Domain.Database.Repositories.DTOs.ProductDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;

namespace PrintingHouse.Infrastructure.Services.Books.Abstactions
{
    public interface IProductService
    {
        Task<ServiceResponse<Product>> GetProductByID(int productId);
        Task<ServiceResponse<List<Product>>> GetProductList();

        Task<ServiceResponse<List<Product>>> GetProductByType(ProductType productType);
        Task<ServiceResponse<List<Product>>> GetProductByAuthor(int authorId);
        Task<ServiceResponse<int>> CreateProduct(CreateProductDTO createProductDTO);
        Task<ServiceResponse<bool>> DeleteProduct(int productId);
        Task<ServiceResponse<bool>> UpdateProduct(UpdateProductDTO updateProductDTO, int productId);
    }
}

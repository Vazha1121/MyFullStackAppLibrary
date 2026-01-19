using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.ProductDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Books.Abstactions;

namespace PrintingHouse.Infrastructure.Services.Books
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<int>> CreateProduct(CreateProductDTO createProductDTO)
        {
            var result = new ServiceResponse<int>();


            var product = new CreateProductDTO()
            {
                ProductName = createProductDTO.ProductName,
                Desctription = createProductDTO.Desctription,
                ProductType = createProductDTO.ProductType,
                ImageUrl = createProductDTO.ImageUrl,
                ISBN = createProductDTO.ISBN,
                publisherId = createProductDTO.publisherId,


            };
            var createdProductId = await _productRepository.CreateProduct(createProductDTO);

            if(createdProductId > 0)
            {
                result.IsSuccess = true;
                result.Data = createdProductId;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Couldn't create new item in database";
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            var result = new ServiceResponse<bool>();

            var deletedProduct = await _productRepository.DeleteProduct(productId);

            if (deletedProduct)
            {
                result.IsSuccess = true;
                result.Data = true;
            }
            else
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = $"Delete of item with ID {productId} failed";
            }

            return result;
        }

        public async Task<ServiceResponse<Product>> GetProductByID(int productId)
        {
            var result = new ServiceResponse<Product>();

            var item = await _productRepository.GetProductByID(productId);

            if(item == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Couldn't find item with ID {productId}";
            }
            else
            {
                result.IsSuccess = true;
                result.Data = item;
            }

            return result;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductList()
        {
            var result = new ServiceResponse<List<Product>>();  

            var products = await _productRepository.GetAllProducts();

            if(products == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Database failure";
            }
            else if(products.Count == 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Couldn't find items in database.";
            }
            else
            {
                result.IsSuccess = true;
                result.Data = products;
            }

            return result;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductByType(ProductType productType)
        {
            var result = new ServiceResponse<List<Product>>();

            var productByType = await _productRepository.GetProductByType(productType);
            if(productByType == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Database faulure";
            }
            else
            {
                result.IsSuccess = true;
                result.Data = productByType;
            }

            return result;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductByAuthor(int authorId)
        {
            var result = new ServiceResponse<List<Product>>();
            var productByAuthor = await _productRepository.GetProductByAuthor(authorId);

            if(productByAuthor == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "No products found";
            }
            else
            {
                result.IsSuccess = true;
                result.Data = productByAuthor;
            }
            return result;
            
        }

        public async Task<ServiceResponse<bool>> UpdateProduct(UpdateProductDTO updateProductDTO, int productId)
        {
            var result = new ServiceResponse<bool>();

            updateProductDTO = new UpdateProductDTO()
            {
                ProductName = updateProductDTO.ProductName,
                Desctription = updateProductDTO.Desctription,
                ProductType = updateProductDTO.ProductType,
                ISBN = updateProductDTO.ISBN
            };

            var updatedResult = await _productRepository.UpdateProduct(updateProductDTO, productId);

            if (updatedResult)
            {
                result.IsSuccess = true;
                result.Data = true;
            }
            else
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = $"Update of item with ID {productId} failed";
            }

            return result;
        }
    }
}

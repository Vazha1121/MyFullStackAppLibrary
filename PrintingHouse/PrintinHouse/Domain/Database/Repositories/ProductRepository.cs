using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.ProductDTO;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateProduct(CreateProductDTO createProductDTO)
        {
            var result = default(int);

            var product = new Product()
            {
                ProductName = createProductDTO.ProductName,
                Desctription = createProductDTO.Desctription,
                ProductType = createProductDTO.ProductType,
                ImageUrl = createProductDTO.ImageUrl,
                ISBN = createProductDTO.ISBN,
                publisherId =  createProductDTO.publisherId,
                
                
            };
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            result = product.ProductId;
            return result;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var result = default(bool);

            var productRemove = await _dbContext.Products.FirstOrDefaultAsync(o => o.ProductId == productId);

            if(productRemove == null)
            {
                result = false;
            }
            else
            {
                _dbContext.Products.Remove(productRemove);
                await _dbContext.SaveChangesAsync();

                result = true;
            }

            return result;


        }

        public async Task<List<Product>> GetAllProducts()
        {
           var result = new List<Product>();    
           result = await _dbContext.Products.ToListAsync();

           return result;
        }
        public async Task<List<Product>> GetProductByType(ProductType productType)
        {
   

          var result = await _dbContext.Products.Where(p => p.ProductType == productType).ToListAsync();

            return result;
        }

        public async Task<List<Product>> GetProductByAuthor(int authorId)
        {
            var result = await _dbContext.AuthorProduct.Where(a => a.AuthorId == authorId)
                                                         .Select(a => a.Book).ToListAsync();

            return result;
        }

        public async Task<Product> GetProductByID(int productId)
        {
            var result = new Product();
            result = await _dbContext.Products.FirstOrDefaultAsync(o => o.ProductId == productId);
            return result;
        }

        public async Task<bool> UpdateProduct(UpdateProductDTO updateProductDTO, int productId)
        {
            var result = default(bool);

            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(o => o.ProductId == productId);

            if(productToUpdate == null)
            {
                result = false; 
            }
            else
            {
                productToUpdate.ProductName = updateProductDTO.ProductName;
                productToUpdate.Desctription = updateProductDTO.Desctription;
                productToUpdate.ProductType = updateProductDTO.ProductType;


                await _dbContext.SaveChangesAsync();
                result = true;
            }
            return result;

        }
    }
}

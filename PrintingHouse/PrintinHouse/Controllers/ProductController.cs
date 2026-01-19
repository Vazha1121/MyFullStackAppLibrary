using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PrintingHouse.Domain.Database.Repositories.DTOs.ProductDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Books.Abstactions;
using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Database.Data;

namespace PrintingHouse.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _dbContext;
        public ProductController(IProductService productService, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            _productService = productService;
            _env = env;
            _dbContext = dbContext;
        }

        [HttpGet("item/{productId:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByID(int productId)
        {
            var response = await _productService.GetProductByID(productId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-all-product")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductList()
        {
            var response = await _productService.GetProductList();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

            
        }
        [HttpGet("getProductType/{productType}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByType(ProductType productType)
        {
            var response = await _productService.GetProductByType(productType);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("author/{authorId}/products")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByAuthor(int authorId)
        {
            var response = await _productService.GetProductByAuthor(authorId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("create-product")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateProduct([FromForm]CreateProductDTO createProductDTO)
        {

            if(createProductDTO.Image != null)
            {
                var imageUrl = await SaveImage(createProductDTO.Image);
                createProductDTO.ImageUrl = imageUrl;
            }
            var response = await _productService.CreateProduct(createProductDTO);



            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }



        [Authorize(Roles = "Manager")]
        [HttpPost("update-item/{itemID:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateProduct(UpdateProductDTO updateProductDTO, int productId)
        {

            if (updateProductDTO.Image != null)
            {
                var imageUrl = await SaveImage(updateProductDTO.Image);
                updateProductDTO.ImageUrl = imageUrl;
            }
            var response = await _productService.UpdateProduct(updateProductDTO, productId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "product-images");

            if (Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return $"product-images/" + fileName;

        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("delete-item/{productId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int productId)
        {
            var response = await _productService.DeleteProduct(productId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("searchProduct")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> SearchProduct([FromQuery] string query)
        {
            var response = new ServiceResponse<List<Product>>();

            var product = await _dbContext.Products.Where(a => a.ProductName.Contains(query) || a.ISBN.Contains(query))
                .ToListAsync();

            if (string.IsNullOrWhiteSpace(query))
            {


                response.IsSuccess = false;
                response.ErrorMessage = "item wasn't found";
                return BadRequest(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Data = product;
                return Ok(response);
            }
        }
    }
}

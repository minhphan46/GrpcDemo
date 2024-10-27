using AutoMapper;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Catalog.API.Dtos.ApiResponse;
using System.Net;
using Catalog.API.Validators;
using Catalog.API.Dtos;
using Catalog.API.Models;

namespace Catalog.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly ProductRepository productRepository;

        public ProductController(IMapper mapper, ProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var products = await productRepository.GetAllAsync(sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            var apiResponse = new APISucessResponse(
                    statusCode: HttpStatusCode.OK,
                    message: "Successfully get all products",
                    data: products
                );

            return Ok(apiResponse);
        }

        // GET: /api/product/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully get product by id",
                data: product
            );

            // Map Domain Model to DTO
            return Ok(apiResponse);
        }

        // CREATE Product
        // POST: /api/product
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddProductDto addProductDto)
        {
            // Map DTO to Domain Model
            var product = mapper.Map<Product>(addProductDto);

            await productRepository.CreateAsync(product);

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.Created,
                message: "Successfully created product",
                data: product
            );

            // Map Domain model to DTO
            return Ok(apiResponse);
        }

        // Update Product By Id
        // PUT: /api/products/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddProductDto addProductDto)
        {

            // Map DTO to Domain Model
            var product = mapper.Map<Product>(addProductDto);

            product = await productRepository.UpdateAsync(id, product);

            if (product == null)
            {
                return NotFound();
            }

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully updated product",
                data: product
            );

            // Map Domain Model to DTO
            return Ok(apiResponse);
        }


        // Delete a Product By Id
        // DELETE: /api/products/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var product = await productRepository.DeleteAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully deleted product",
                data: product
            );

            // Map Domain Model to DTO
            return Ok(apiResponse);
        }
    }
}

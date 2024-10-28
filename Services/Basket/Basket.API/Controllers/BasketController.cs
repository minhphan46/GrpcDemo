using AutoMapper;
using Basket.API.Dtos;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Basket.API.Dtos.ApiResponse;

namespace Basket.API.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketRepository basketRepository;

        public BasketController(BasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carts = await basketRepository.GetCartsAsync();

            var apiResponse = new APISucessResponse(
                    statusCode: HttpStatusCode.OK,
                    message: "Successfully get all carts",
                    data: carts
                );

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] String userId)
        {
            var carts = await basketRepository.GetCartsByUserIdAsync(Guid.Parse(userId));

            if (carts == null)
            {
                return NotFound();
            }

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully get cart by userId",
                data: carts
            );

            // Map Domain Model to DTO
            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartDto cartDto)
        {
            // Map DTO to Domain Model
            var cart = await basketRepository.CreateAsync(cartDto);

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.Created,
                message: "Successfully created cart",
                data: cart
            );

            // Map Domain model to DTO
            return Ok(apiResponse);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CartDto cartDto)
        {
            // Map DTO to Domain Model
            var cart = await basketRepository.UpdateAsync(id, cartDto);

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully updated cart",
                data: cart
            );

            // Map Domain model to DTO
            return Ok(apiResponse);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var cart = await basketRepository.DeleteAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully deleted cart",
                data: cart
            );

            return Ok(apiResponse);
        }
    }
}

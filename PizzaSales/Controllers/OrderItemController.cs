using Microsoft.AspNetCore.Mvc;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Services;
using PizzaSalesAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PizzaSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadOrders(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = file.OpenReadStream())
            {
                await _orderItemService.GetDataFromCsv(stream);
            }

            return Ok("Order items processed successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize)
        {
            var orderItems = await _orderItemService.GetPaginated(pageNumber, pageSize);
            var orderItemsDto = orderItems.Select(orderItem => new OrderItemDto
            {
                Id = orderItem.Id,
                PizzaId = orderItem.PizzaId,
                Quantity = orderItem.Quantity,
                Pizza = new PizzaDto
                {
                    PizzaTypeId = orderItem.Pizza.PizzaTypeId,
                    Price = orderItem.Pizza.Price,
                    Size = orderItem.Pizza.Size,

                }
                
            }).ToList();
            return Ok(orderItemsDto);
        }
    }
}

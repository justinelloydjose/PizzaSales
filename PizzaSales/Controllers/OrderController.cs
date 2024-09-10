using Microsoft.AspNetCore.Mvc;
using PizzaSales.Models;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PizzaSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
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
                await _orderService.GetDataFromCsv(stream);
            }

            return Ok("Orders processed successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] [Required] int pageNumber, [FromQuery] [Required] int pageSize)
        {
            var orders = await _orderService.GetPaginated(pageNumber, pageSize);
            var ordersDto = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    PizzaId = item.PizzaId,
                    Quantity = item.Quantity,
                    Pizza = new PizzaDto
                    {
                        PizzaTypeId = item.Pizza.PizzaTypeId,
                        Price = item.Pizza.Price,
                        Size = item.Pizza.Size,

                    }
                }).ToList()
            }).ToList();
            return Ok(ordersDto);
        }

    }
}

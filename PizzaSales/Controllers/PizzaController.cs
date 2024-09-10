using Microsoft.AspNetCore.Mvc;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Services;
using PizzaSalesAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PizzaSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
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
                await _pizzaService.GetDataFromCsv(stream);
            }

            return Ok("List of pizzas processed successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzas([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize)
        {
            var pizzas = await _pizzaService.GetPaginated(pageNumber, pageSize);
            var pizzasDto = pizzas.Select(pizza => new PizzaDto
            {
                PizzaId = pizza.Id,
                PizzaTypeId = pizza.PizzaTypeId,
                Price = pizza.Price,
                Size = pizza.Size
            }).ToList();
            return Ok(pizzas);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Services;
using PizzaSalesAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PizzaSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaTypeController : ControllerBase
    {
        private readonly IPizzaTypeService _pizzaTypeService;
        public PizzaTypeController(IPizzaTypeService pizzaTypeService)
        {
            _pizzaTypeService = pizzaTypeService;
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
                await _pizzaTypeService.GetDataFromCsv(stream);
            }

            return Ok("List of pizza types processed successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaTypes([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize)
        {
            var pizzaTypes = await _pizzaTypeService.GetPaginated(pageNumber, pageSize);
            var pizzaTypesDto = pizzaTypes.Select(type => new PizzaTypeDto
            {
                Category = type.Category,
                Id = type.Id,
                Ingredients = type.Ingredients,
                Name = type.Name
            }).ToList();
            return Ok(pizzaTypes);
        }
    }
}

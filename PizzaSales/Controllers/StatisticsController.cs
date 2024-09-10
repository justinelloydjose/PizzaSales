using Microsoft.AspNetCore.Mvc;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Services;
using PizzaSalesAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PizzaSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }


        [HttpGet("top-pizza-sales")]
        public async Task<IActionResult> GetTopPizzaSales([FromQuery][Required] int topCount)
        {
            var pizzas = await _statisticsService.GetTopPizzaSalesAsync(topCount);
            
            return Ok(pizzas);
        }

        [HttpGet("top-monthly-sales")]
        public async Task<IActionResult> GetTopMonthlySales([FromQuery] int topCount)
        {
            var topMonthlySales = await _statisticsService.GetTopMonthlySalesAsync(topCount);
            return Ok(topMonthlySales);
        }
    }
}

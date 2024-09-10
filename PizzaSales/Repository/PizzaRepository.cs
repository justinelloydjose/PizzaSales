using Microsoft.EntityFrameworkCore;
using PizzaSales.Models;
using PizzaSalesAPI.Data;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Repository.Interfaces;

namespace PizzaSalesAPI.Repository
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly DataContext _context;

        public PizzaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Pizza pizza)
        {
            await _context.Pizzas.AddAsync(pizza);
            await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Pizza> pizzas)
        {
            await _context.Pizzas.AddRangeAsync(pizzas);
        }

        public async Task<List<Pizza>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Pizzas
              .OrderBy(p => p.Id) // Adjust ordering as needed
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync();
        }

        public async Task<List<MonthlySalesDto>> GetTopMonthlySalesAsync(int topCount)
        {
            var topMonthlySales = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Pizza)
                .GroupBy(oi => new
            {
                Year = oi.Order.CreatedAt.Year,
                Month = oi.Order.CreatedAt.Month
            }) // Group by Year and Month
            .Select(group => new
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                TotalSales = group.Sum(oi => oi.Quantity * oi.Pizza.Price) // Calculate total sales
            })
            .OrderByDescending(ms => ms.TotalSales) // Order by total sales in descending order
            .Take(topCount) // Take the top `topCount` months
            .ToListAsync();

            // Map to DTOs
            var topMonthlySalesDto = topMonthlySales.Select(ms => new MonthlySalesDto
            {
                Year = ms.Year,
                Month = ms.Month,
                TotalSales = ms.TotalSales
            }).ToList();

            return topMonthlySalesDto;
        }


        public async Task<List<PizzaSaleDto>> GetTopPizzaSales(int topCount)
        {
            var topPizzas = await _context.OrderItems
                .GroupBy(oi => oi.PizzaId)
                 .Select(group => new
                 {
                     PizzaId = group.Key,
                     TotalSales = group.Sum(oi => oi.Quantity) // Aggregate sales
                 })
                .OrderByDescending(p => p.TotalSales) // Order by total sales in descending order
                .Take(topCount) // Take the top 5
                .ToListAsync();

            var topPizzasDto = topPizzas.Select(p => new PizzaSaleDto
            {
                PizzaId = p.PizzaId,
                TotalSales = p.TotalSales
            }).ToList();

            return topPizzasDto;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

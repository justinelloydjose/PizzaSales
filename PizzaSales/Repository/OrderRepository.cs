using Microsoft.EntityFrameworkCore;
using PizzaSales.Models;
using PizzaSalesAPI.Data;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Shared;

namespace PizzaSalesAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
           _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task AddRangeAsync(IEnumerable<Order> orders)
        {
            await _context.Orders.AddRangeAsync(orders);
        }

        public async Task<List<Order>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
            .OrderBy(o => o.CreatedAt) // Adjust ordering as needed
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}

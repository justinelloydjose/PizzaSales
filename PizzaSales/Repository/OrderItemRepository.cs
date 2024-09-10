using Microsoft.EntityFrameworkCore;
using PizzaSales.Models;
using PizzaSalesAPI.Data;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Shared;

namespace PizzaSalesAPI.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _context;
        public OrderItemRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }

        public async Task AddRangeAsync(IEnumerable<OrderItem> orderItems)
        {
            await _context.OrderItems.AddRangeAsync(orderItems);
        }

        public async Task<List<OrderItem>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.OrderItems
              .Include(o => o.Pizza)
              .OrderBy(oi => oi.Order.CreatedAt) // Adjust ordering as needed
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

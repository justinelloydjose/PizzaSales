using Microsoft.EntityFrameworkCore;
using PizzaSales.Models;
using PizzaSalesAPI.Data;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Shared;

namespace PizzaSalesAPI.Repository
{
    public class PizzaTypeRepository : IPizzaTypeRepository
    {
        private readonly DataContext _context;
        public PizzaTypeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PizzaType pizzaType)
        {
            await _context.PizzaTypes.AddAsync(pizzaType);
            await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<PizzaType> pizzaTypes)
        {
            await _context.PizzaTypes.AddRangeAsync(pizzaTypes);
        }

        public async Task<List<PizzaType>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.PizzaTypes
              .OrderBy(pt => pt.Id) // Adjust ordering as needed
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

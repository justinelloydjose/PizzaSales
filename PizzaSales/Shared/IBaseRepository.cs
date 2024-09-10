using PizzaSales.Models;

namespace PizzaSalesAPI.Shared
{
    public interface IBaseRepository<T> where T : class
    {
        //Adding single entity data to the entity repository
        Task AddAsync(T entity);

        //Adding bulk entity data to the entity repository
        Task AddRangeAsync(IEnumerable<T> entities);


        //SaveChangesAsync after the batch insert to persist changes to the database.
        Task SaveChangesAsync();

        //Get orders with pagination, default sorting by date (oldest to latest)
        Task <List<T>> GetPagedAsync(int page, int pageSize);
    }
}

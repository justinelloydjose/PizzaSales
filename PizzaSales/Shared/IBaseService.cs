namespace PizzaSalesAPI.Shared
{
    public interface IBaseService<T> where T : class
    {
        Task GetDataFromCsv(Stream csvStream);
        Task<List<T>> GetPaginated(int pageNumber, int pageSize);
    }
}

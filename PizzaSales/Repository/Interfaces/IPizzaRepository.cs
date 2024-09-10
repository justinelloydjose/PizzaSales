using PizzaSales.Models;
using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Shared;

namespace PizzaSalesAPI.Repository.Interfaces
{
    public interface IPizzaRepository : IBaseRepository<Pizza>
    {
        //insert specific properties
        Task<List<PizzaSaleDto>> GetTopPizzaSales(int topCount);

        Task<List<MonthlySalesDto>> GetTopMonthlySalesAsync(int topCount);
    }
}

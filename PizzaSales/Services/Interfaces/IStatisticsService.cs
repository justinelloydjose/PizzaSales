using PizzaSalesAPI.Dto;

namespace PizzaSalesAPI.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<PizzaDto> GetHighestPizzaSaleAsync();
        Task<List<PizzaSaleDto>> GetTopPizzaSalesAsync(int topCount);
        Task<List<MonthlySalesDto>> GetTopMonthlySalesAsync(int topCount);
    }
}

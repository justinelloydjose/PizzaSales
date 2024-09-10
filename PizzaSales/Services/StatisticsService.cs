using PizzaSalesAPI.Dto;
using PizzaSalesAPI.Repository;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Services.Interfaces;

namespace PizzaSalesAPI.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public StatisticsService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }
        public Task<PizzaDto> GetHighestPizzaSaleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PizzaSaleDto>> GetTopPizzaSalesAsync(int dataSize)
        {
            return await _pizzaRepository.GetTopPizzaSales(dataSize);
        }

        public async Task<List<MonthlySalesDto>> GetTopMonthlySalesAsync(int topCount)
        {
            return await _pizzaRepository.GetTopMonthlySalesAsync(topCount);
        }
    }
}

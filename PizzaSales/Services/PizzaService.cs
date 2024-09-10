using CsvHelper.Configuration;
using CsvHelper;
using PizzaSales.Models;
using PizzaSalesAPI.Dto.CSV;
using PizzaSalesAPI.Helper;
using PizzaSalesAPI.Repository;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Services.Interfaces;
using System.Globalization;

namespace PizzaSalesAPI.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }
        public async Task GetDataFromCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<PizzaDtoMap>();

                var pizzas = new List<Pizza>();
                while (await csv.ReadAsync())
                {
                    var row = csv.GetRecord<PizzaDtoCsv>();
                    string priceAsString = row.Price.ToString();

                    var pizza = new Pizza
                    {
                        Id = row.PizzaId,
                        PizzaTypeId = row.PizzaTypeId,
                        Size = row.Size,
                        Price = decimal.Parse(priceAsString)
                    };

                    pizzas.Add(pizza);
                }
                await _pizzaRepository.AddRangeAsync(pizzas);
                await _pizzaRepository.SaveChangesAsync();
            }
        }

        public async Task<List<Pizza>> GetPaginated(int pageNumber, int pageSize)
        {
            //pageNumber cant be 0; default value is 1;
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            //pageSize cant be less than 10; default value is 10;
            if (pageSize < 10)
            {
                pageSize = 10;
            }

            return await _pizzaRepository.GetPagedAsync(pageNumber, pageSize);
        }
    }
}

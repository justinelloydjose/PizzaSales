using CsvHelper.Configuration;
using CsvHelper;
using PizzaSales.Models;
using PizzaSalesAPI.Helper;
using PizzaSalesAPI.Repository;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Shared;
using System.Globalization;
using PizzaSalesAPI.Services.Interfaces;
using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Services
{
    public class PizzaTypeService : IPizzaTypeService
    {
        private readonly IPizzaTypeRepository _pizzaTypeRepository;
        public PizzaTypeService(IPizzaTypeRepository pizzaTypeRepository)
        {
            _pizzaTypeRepository = pizzaTypeRepository;
        }
        public async Task GetDataFromCsv(Stream csvStream)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<PizzaTypeDtoMap>();

                var pizzaTypes = new List<PizzaType>();
                while (await csv.ReadAsync())
                {
                    var row = csv.GetRecord<PizzaTypeDtoCsv>();

                    var pizzaType = new PizzaType
                    {
                        Id = row.PizzaTypeId,
                        Name = row.Name,
                        Category = row.Category,
                        Ingredients = row.Ingredients
                    };

                    pizzaTypes.Add(pizzaType);
                }
                await _pizzaTypeRepository.AddRangeAsync(pizzaTypes);
                await _pizzaTypeRepository.SaveChangesAsync();
            }
        }

        public async Task<List<PizzaType>> GetPaginated(int pageNumber, int pageSize)
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

            return await _pizzaTypeRepository.GetPagedAsync(pageNumber, pageSize);
        }
    }
}

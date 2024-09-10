using CsvHelper.Configuration;
using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Helper
{
    public class PizzaTypeDtoMap : ClassMap<PizzaTypeDtoCsv>
    {
        public PizzaTypeDtoMap()
        {
            Map(m => m.PizzaTypeId).Name("pizza_type_id");
            Map(m => m.Name).Name("name");
            Map(m => m.Category).Name("category");
            Map(m => m.Ingredients).Name("ingredients");
        }
    }
}

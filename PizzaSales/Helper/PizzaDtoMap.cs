using CsvHelper.Configuration;
using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Helper
{
    public class PizzaDtoMap : ClassMap<PizzaDtoCsv>
    {
        public PizzaDtoMap()
        {
            Map(m => m.PizzaId).Name("pizza_id");
            Map(m => m.PizzaTypeId).Name("pizza_type_id");
            Map(m => m.Size).Name("size");
            Map(m => m.Price).Name("price");
        }
    }
}

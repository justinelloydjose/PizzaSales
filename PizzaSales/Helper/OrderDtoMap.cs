using CsvHelper.Configuration;
using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Helper
{
    public class OrderDtoMap : ClassMap<OrderDtoCsv>
    {
        public OrderDtoMap()
        {
            Map(m => m.OrderId).Name("order_id");
            Map(m => m.Date).Name("date");
            Map(m => m.Time).Name("time");
        }
    }
}

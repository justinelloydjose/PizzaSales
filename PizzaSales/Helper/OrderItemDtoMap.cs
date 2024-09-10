using CsvHelper.Configuration;
using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Helper
{
    public class OrderItemDtoMap : ClassMap<OrderItemDtoCsv>
    {
        public OrderItemDtoMap()
        {
            Map(m => m.OrderDetailsId).Name("order_details_id");
            Map(m => m.OrderId).Name("order_id");
            Map(m => m.PizzaId).Name("pizza_id");
            Map(m => m.Quantity).Name("quantity");
        }
    }
}

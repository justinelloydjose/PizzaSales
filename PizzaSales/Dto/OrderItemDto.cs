using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Dto
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PizzaId { get; set; }
        public int Quantity { get; set; }

        public OrderDto Order { get; set; }
        public PizzaDto Pizza { get; set; }
    }
}

using PizzaSalesAPI.Dto.CSV;

namespace PizzaSalesAPI.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}

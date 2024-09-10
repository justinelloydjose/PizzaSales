using System.Collections;

namespace PizzaSales.Models
{
    public class Order
    {
        public int Id { get; set; }

        private readonly List<OrderItem> _orderItems = new();

        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the list of order items.
        /// </summary>
        public IEnumerable<OrderItem> OrderItems => _orderItems.AsReadOnly();
    }
}

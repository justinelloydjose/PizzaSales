namespace PizzaSales.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PizzaId {  get; set; }
        public int Quantity { get; set; }

        //To navigate Order
        public Order Order { get; set; }
        public Pizza Pizza { get; set; }

    }
}

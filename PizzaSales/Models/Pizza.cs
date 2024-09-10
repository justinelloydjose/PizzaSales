namespace PizzaSales.Models
{
    public class Pizza
    {
        public string Id { get; set; }
        public string PizzaTypeId { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }
}

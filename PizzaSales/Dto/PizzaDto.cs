namespace PizzaSalesAPI.Dto
{
    public class PizzaDto
    {
        public string PizzaId { get; set; }
        public string Name { get; set; } // Add more properties as needed
        public string PizzaTypeId { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }
}

namespace PizzaSalesAPI.Dto.CSV
{
    public class OrderDtoCsv
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}

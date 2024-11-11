namespace PentiaCaseApi.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int SalespersonId { get; set; }
        public string OrderDate { get; set; }
        public decimal Amount { get; set; }

    }
}

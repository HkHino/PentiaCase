namespace PentiaCaseApi.Models
{
    public class Salesperson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string hireDate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public List<Orders> Orders { get; set; }
        public int OrderCount { get; set; }  
    }
}

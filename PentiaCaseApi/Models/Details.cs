using Microsoft.AspNetCore.Mvc;

namespace PentiaCaseApi.Models
{
    public class Details
    {
        public string name = "bob";
        public List<Orders> orders = new List<Orders>();
    }
 
}

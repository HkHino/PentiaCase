using PentiaCaseApi.Models;
using PentiaCaseApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace PentiaCaseApi.Controllers
{
    public class SalespeopleController : Controller
    {
        private readonly ApiSettings _apiSettings;

        public SalespeopleController(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        // /salesperson
        public IActionResult Index()
        {
            try
            {
                var client = new HttpClient();
                var c = new ApiService(client, _apiSettings);
                             
                var salespeople = c.GetSalespeopleAsync().Result;
                var orders = c.GetOrdersAsync().Result;
               
                foreach (var salesperson in salespeople)
                {
                    salesperson.Orders = orders.Where(o => o.SalespersonId == salesperson.id).ToList(); 
                    salesperson.OrderCount = salesperson.Orders.Count; 
                }
                                
                return View(salespeople);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View("Error");
            }
        }


        // /salesperson/id
        public IActionResult Details(int id)
        {
            try
            {
                ApiSettings s = new ApiSettings();
                s.ApiKey = "test1234";
                s.BaseUrl = "https://azurecandidatetestapi.azurewebsites.net/api/v1.0/";
                var client = new HttpClient();
                var c = new ApiService(client, s);
                var ls = c.GetSalespeopleAsync().Result;

                if (ls == null || !ls.Any())
                {
                    return NotFound("Salespeople data could not be retrieved");
                }

                var salesperson = ls.Find(x => x.id == id);
                if (salesperson == null)
                {
                    return NotFound($"Salesperson with {id} not found");
                }

                var orders = c.GetOrdersAsync().Result;
                
                var test = new Details()
                {
                    name = salesperson.name,
                    orders = orders
                };

                return View(test);
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"an error occured: { ex.Message}");
            }
        
        }

        // /salesperson/monthlyvolumes
        public IActionResult MonthlyVolumes()
        {
            ApiSettings s = new ApiSettings();
            s.ApiKey = "test1234";
            s.BaseUrl = "https://azurecandidatetestapi.azurewebsites.net/api/v1.0/";
            var client = new HttpClient();
            var c = new ApiService(client, s);

            var orders = c.GetOrdersAsync().Result;

            var validOrders = orders.Where(o => !string.IsNullOrEmpty(o.OrderDate)).ToList();

            var monthlyVolumes = validOrders
            .GroupBy(o => o.OrderDate.Length >= 7 ? o.OrderDate.Substring(0, 7) : "")
            .Where(g => !string.IsNullOrEmpty(g.Key)) 
            .Select(g => new MonthlyVolume
            {
            month = g.Key,
            OrderCount = g.Count() 
        }).ToList();

            return View(monthlyVolumes);
        }


    }
}


using PentiaCaseApi.Models;
using PentiaCaseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace PentiaCaseApi.Controllers
{
    public class SalespeopleController : Controller
    {
        public IActionResult Index()
        {
            ApiSettings s = new ApiSettings();
            s.ApiKey = "test1234";
            s.BaseUrl = "https://azurecandidatetestapi.azurewebsites.net/api/v1.0/";
            var client = new HttpClient();
            var c = new ApiService(client, s);

            return View();
        }
    }
}

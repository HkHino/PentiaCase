using PentiaCaseApi.Models;

namespace PentiaCaseApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
                       
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.MapControllerRoute(
                name: "details",
                pattern: "{controller=Salespeople}/{action=Details}/{id}");

            app.Run();
        }
    }
}

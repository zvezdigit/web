namespace SMS.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Models.Products;
    using SMS.Models.Users;
    using System.Linq;


    public class HomeController : Controller
    {
        private readonly Data.SMSDbContext context;

        public HomeController(Data.SMSDbContext context)
        {
            this.context = context;
        }

        public HttpResponse Index()
        {
            if (User?.IsAuthenticated == true)
            {
                var user = context.Users
                    .Where(x => x.Id == User.Id)
                    .First();

                var products = context.Products.ToList();

                var viewModel = new IndexLoggedInModel()
                {
                    Username = user.Username,
                    Products = products.Select(p => new ProductViewModel 
                    { 
                        Id = p.Id, 
                        Name = p.Name, 
                        Price = p.Price 
                    }).ToList(),
                };

                return View(viewModel, "IndexLoggedIn");
            }

            return View();
        }
    }
}
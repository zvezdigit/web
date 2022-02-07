using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Models.Carts;
using SMS.Models.Products;
using System.Linq;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly Data.SMSDbContext context;

        public CartsController(Data.SMSDbContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        [Authorize]
        public HttpResponse Details()
        {
            var products = context.Users
                .Include(u => u.Cart)
                .ThenInclude(u => u.Products)
                .First()
                .Cart.Products.ToList();

            var viewModel = new CartDetailsViewModel
            {
                Products = products.Select(p => new ProductViewModel
                {
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            };
                
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public HttpResponse AddProduct(string productId)
        {
            var user = context.Users.Include(x => x.Cart).ThenInclude(x => x.Products).First(x => x.Id == User.Id);
            var product = context.Products.First(x => x.Id == productId);

            user.Cart.Products.Add(product);

            context.SaveChanges();

            return Redirect("/Carts/Details");
        }

        [HttpGet]
        [Authorize]
        public HttpResponse Buy()
        {
            var userCart = context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .Where(c=>c.Id==User.Id)
                .First().Cart;

            userCart.Products.Clear();

            context.SaveChanges();
            
            return Redirect("/Home");
        }

    }
}

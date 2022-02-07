using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models;
using SMS.Models.Products;
using SMS.Services;
using System.Linq;

namespace SMS.Controllers
{
    public class ProductsController: Controller
    {
        private readonly IValidator validator;
        private readonly SMSDbContext data;

        public ProductsController(IValidator validator,  SMSDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }
        [Authorize]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateProductFormModel model)
        {
            var modelErrors = this.validator.ValidateProduct(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
               CartId = this.data.Users.First(i => i.Id == User.Id).CartId 
            };

            data.Products.Add(product);

            data.SaveChanges();

            return Redirect("/Home");
        }

        //[HttpPost]
        //[Authorize]
        //public HttpResponse Add(AddProductsFormModel model)
        //{
        //    var cart = data.Users
        //           .Where(c => c.Id == User.Id)
        //           .Include(x => x.Cart)
        //           .ThenInclude(x => x.Products)
        //           .First()
        //           .Cart;

        //    var products = model.Products.Select(x => new Product { Name = x.Name, Price = x.Price }).ToList();

        //    foreach(var product in products)
        //    {
        //        cart.Products.Add(product);
        //    }

        //    data.SaveChanges();

        //    return Redirect("/Cart/Details");
        //}
    }
}

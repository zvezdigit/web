using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModel.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;

        private readonly IPasswordHasher hasher;

        private readonly ApplicationDbContext data;

        public UsersController(IValidator validator, IPasswordHasher hasher, ApplicationDbContext data)
        {
            this.validator = validator;
            this.hasher = hasher;
            this.data = data;
        }
        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserModelForm model)
        {
            var modelErrors = this.validator.ValidateUser(model);

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' username already exists.");
            }

            if (this.data.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with '{model.Email}' e-mail already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = hasher.HashPassword(model.Password)
            };

            data.Users.Add(user);

            data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this.hasher.HashPassword(model.Password);


            var userId = this.data
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse LogOut()
        {

            this.SignOut();

            return Redirect("/");
        }
    }
}

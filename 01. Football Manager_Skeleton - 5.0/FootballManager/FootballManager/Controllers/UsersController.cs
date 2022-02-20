using FootballManager.Contracts;
using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Controllers
{

    public class UsersController: Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly FootballManagerDbContext data;

        public UsersController(IValidator _validator, IPasswordHasher _passwordHasher, FootballManagerDbContext _data)
        {
            validator = _validator;
            this.passwordHasher = _passwordHasher;  
            this.data = _data;
        }

        public HttpResponse Register()
        {
            if (User?.IsAuthenticated==true)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
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
               // return Error(modelErrors);
                return Redirect("/Users/Register");
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.PasswordHasher(model.Password),
                Email = model.Email,
            };

            try
            {
                data.Users.Add(user);

                data.SaveChanges();
            }
            catch (Exception)
            {

                //return Error("User could not be saved in DB.");
                return Redirect("/Users/Register");
            }



            return Redirect("/Users/Login");

        }

        public HttpResponse Login()
        {
            if (User?.IsAuthenticated == true)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this.passwordHasher.PasswordHasher(model.Password);

            var userId = this.data
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
               // return Error("Username and password combination is not valid.");
                return Redirect("/Users/Login");
            }

            this.SignIn(userId);

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }

    }
}

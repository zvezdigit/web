using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModel.Repositories;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Git.Data.DataConstants;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private ApplicationDbContext data;

        public RepositoriesController(IValidator validator, ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

      
        public HttpResponse All()
        {

            var repositories = data.Repositories
                .Where(r=>r.OwnerId==User.Id || r.IsPublic==true)
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner,
                    CreatedOn = r.CreatedOn,
                    Commits = r.Commits,
                })
                .ToList();

            var viewModel = new AllRepositoriesFormModel

            {
                Repositories = repositories.Select(r => new RepositoryFormModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner.Username,
                    CreatedOn = r.CreatedOn,
                    CommitsCount = r.Commits.Count()

                }).ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateRepositoryFormModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (this.data.Repositories.Any(u => u.Name == model.Name))
            {
                modelErrors.Add($"Repository with '{model.Name}' name already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == RepositoryTypePublic,
                CreatedOn = DateTime.UtcNow,
                OwnerId = User.Id,
                Commits = new List<Commit>()
            };

            data.Repositories.Add(repository);
            data.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
    
}

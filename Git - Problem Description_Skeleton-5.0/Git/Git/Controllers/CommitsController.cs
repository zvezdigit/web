using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModel.Commits;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public CommitsController(ApplicationDbContext data, IValidator validator)
        {
            
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = data.Commits
                             .Where(c => c.CreatorId == User.Id)
                             .OrderByDescending(c=>c.CreatedOn)
                             .Select(c => new
                             {
                                 Id = c.Id,
                                 Description = c.Description,
                                 CreatedOn = c.CreatedOn,
                                 Repository = c.Repository
                             })
                             .ToList();

            var viewModel = new AllCommitsFormModel

            {
                Commits = commits.Select(c => new CommitFormModel
                {
                    Id = c.Id,
                    Repository = c.Repository.Name,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn,

                }).ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        public HttpResponse Create( string id)
        {
            var repository = data.Repositories
                .First(r => r.Id == id);
          

            var viewModel = new CommitRepositoryFormModel
            {
                Id = repository.Id,
                Name = repository.Name
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            var modelErrors = this.validator.ValidateCommit(model);

            if (this.data.Commits.Any(u => u.Description == model.Description))
            {
                modelErrors.Add($"Commit with '{model.Description}' description already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }


            var commit = new Commit
            {
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = User.Id,
                RepositoryId =model.Id
            };

            data.Commits.Add(commit);
            data.SaveChanges();

            return Redirect("/Repositories/All");
        }

       [HttpGet]
       [Authorize]
        public HttpResponse Delete(string id)
        {
            var commitId = data.Commits
                .Where(c=>c.CreatorId==User.Id && c.Id==id)
                .Select(c => c.Id)
                .FirstOrDefault();

            var commit = data.Commits.First(c => c.Id == commitId);

            data.Commits.Remove(commit);
            data.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}

using Git.Data;
using Git.InputModels;
using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoryService repositoryService;
        private readonly ApplicationDbContext context;
        public RepositoriesController(ApplicationDbContext context, RepositoryService repositoryService)
        {
            this.context = context;
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            var allRepositoriesViewModel = this.repositoryService.GetRepositoriesForUser(this.GetUserId());
            return this.View(allRepositoriesViewModel);
        }

        public HttpResponse Create()
        {
            if(this.IsUserSignedIn() == false)
            {
                return this.Error("Only signed in users can create a repository");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryInputModel inputModel)
        {
            if (this.IsUserSignedIn() == false)
            {
                return this.Error("Only signed in users can create a repository");
            }

            if(string.IsNullOrEmpty(inputModel.Name) || inputModel.Name.Length < 3 || inputModel.Name.Length > 10)
            {
                return this.Error("Repository name should be between 3 and 10 characters long.");
            }

            inputModel.OwnerId = this.GetUserId();

            if (this.repositoryService.RepositoryNameExists(inputModel.Name, inputModel.OwnerId) == true)
            {
                return this.Error("Repository with this name already exists");
            }

            this.repositoryService.CreateRepository(inputModel);

            return this.Redirect("/Repositories/All");
        }
    }
}

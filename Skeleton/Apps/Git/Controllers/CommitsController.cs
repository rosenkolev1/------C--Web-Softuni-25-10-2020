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
    public class CommitsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ICommitService commitService;
        private readonly IRepositoryService repositoryService;
        public CommitsController(ApplicationDbContext context, CommitService commitService, RepositoryService repositoryService)
        {
            this.context = context;
            this.commitService = commitService;
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            if (this.IsUserSignedIn() == false)
            {
                return this.Error("Only logged in users can see all commits");
            }
            var allCommitsViewModel = this.commitService.GetAllCommitsForUser(this.GetUserId());
            return this.View(allCommitsViewModel);
        }

        public HttpResponse Create(string repositoryId)
        {
            if(this.IsUserSignedIn() == false)
            {
                return this.Error("Only logged in users can create commit");
            }
            var createCommitViewModel = this.repositoryService.GetRepositoryNameAndId(repositoryId);
            return this.View(createCommitViewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateCommitInputModel inputModel)
        {
            if (this.IsUserSignedIn() == false)
            {
                return this.Error("Only logged in users can create commit");
            }

            if(string.IsNullOrEmpty(inputModel.Description) || inputModel.Description.Length < 5)
            {
                return this.Error("Description should be at least 5 characters");
            }

            inputModel.CreatorId = this.GetUserId();
            this.commitService.CreateCommit(inputModel);
           
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string commitId)
        {
            if (this.IsUserSignedIn() == false)
            {
                return this.Error("Only logged in users can delete commit");
            }
            var userId = this.GetUserId();

            if(this.commitService.CommitFromUserExists(commitId, userId) == false)
            {
                return this.Error("Commit doesn't exist or you don't have any right to delete the commit");
            }

            this.commitService.DeleteCommit(commitId);

            return this.Redirect("/Commits/All");
        }
    }
}

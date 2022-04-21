using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using provider.InfraStructure.Service;
using provider.Model.GitHub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(GitHubService gitHubService) => _gitHubService = gitHubService;

        [HttpGet]
        public async Task<IEnumerable<GithubBranch>> Get()
        {
            IEnumerable<GithubBranch> result = await _gitHubService.GetAspNetCoreDocsBranchesAsync();
            return result;
        }
    }
}

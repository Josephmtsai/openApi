using Microsoft.Net.Http.Headers;
using provider.Model.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace provider.InfraStructure.Service
{
    public class GitHubService
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public GitHubService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.github.com/");

            // using Microsoft.Net.Http.Headers;
            // The GitHub API requires two headers.
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, "HttpRequestsSample");
        }

        public async Task<IEnumerable<GithubBranch>?> GetAspNetCoreDocsBranchesAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<GithubBranch>>(
                "repos/dotnet/AspNetCore.Docs/branches");
    }
}

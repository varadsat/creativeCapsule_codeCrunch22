using CodeCrunch22.Models.Github;
using CodeCrunch22.Models.StackoverFlow;
using Octokit;
using System.Net.Http.Headers;
using System.Text.Json;
using ProductHeaderValue = System.Net.Http.Headers.ProductHeaderValue;

namespace CodeCrunch22.Services.Github
{
    public class GithubService : IGithubService
    {
        private readonly IHttpClientFactory _clientFactory;

        public readonly string API_KEY;

        public GithubService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            API_KEY = configuration["Github:API_KEY"];
        }
        public async Task<Octokit.SearchRepositoryResult> GetSearchData(string searchString)
        {
            var github = new GitHubClient(new Octokit.ProductHeaderValue("MyAmazingApp"));
            var tokenAuth = new Credentials(API_KEY);
            github.Credentials = tokenAuth;
            var request = new SearchRepositoriesRequest(searchString == "" ? "language" : searchString)
            { PerPage = 10 };
            var result = await github.Search.SearchRepo(request);
            return result;
        }

        public async Task<GithubSearchData> GetSearchData2Async(string searchString)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", API_KEY);
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("MyAmazingApp")));
            var result = await client.GetStringAsync
                ($"https://api.github.com/search/repositories?q={searchString}");
            var returnData = JsonSerializer.Deserialize<GithubSearchData>(result);
            return returnData;
        }
    }
}

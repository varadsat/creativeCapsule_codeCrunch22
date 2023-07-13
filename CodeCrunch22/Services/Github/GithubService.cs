using CodeCrunch22.Models.Github;
using CodeCrunch22.Models.StackoverFlow;
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
            API_KEY = configuration["Github_API_KEY"];
        }

        public async Task<GithubSearchData> GetSearchData2Async(string searchString)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", API_KEY);
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("MyAmazingApp")));
            var term = searchString == "" ? "language" : searchString;
            var result = await client.GetStringAsync
                ($"https://api.github.com/search/repositories?q={term}&per_page=10");
            var returnData = JsonSerializer.Deserialize<GithubSearchData>(result);
            return returnData;
        }
    }
}

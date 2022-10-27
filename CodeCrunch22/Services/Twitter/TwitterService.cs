using CodeCrunch22.Models.Twitter;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeCrunch22.Services.Twitter
{
    public class TwitterService : ITwitterService
    {
        private readonly IHttpClientFactory _clientFactory;

        public readonly string BEARER_TOKEN;

        public TwitterService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            BEARER_TOKEN = configuration["Twitter:BEARER_TOKEN"];
        }
        public async Task<TwitterSearchData> GetTwitterSearchDataAsync(string searchString)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER_TOKEN);
            var term = searchString.Equals("") ? "Nextjs" : searchString;
            var result = await client.GetStringAsync
                ($"https://api.twitter.com/1.1/search/tweets.json?q={term}&result_type=popular&count=10");
            var returnData = JsonSerializer.Deserialize<TwitterSearchData>(result);
            return returnData;
        }
    }
}

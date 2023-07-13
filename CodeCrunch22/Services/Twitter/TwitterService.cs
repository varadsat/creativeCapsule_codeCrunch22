using CodeCrunch22.Models.Google;
using CodeCrunch22.Models.Twitter;
using CodeCrunch22.Models.Twitter.V2;
using Microsoft.AspNetCore.WebUtilities;
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
            BEARER_TOKEN = configuration["Twitter_BEARER_TOKEN"];
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

        public async Task<TwitterSearchDataV2> GetTwitterSearchDataV2Async(string searchString)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER_TOKEN);
            var term = searchString.Equals("") ? "Nextjs" : searchString;
            var queryParams = new Dictionary<string, string>()
            {
                {"query", $"{term} -is:retweet" },
                {"sort_order","relevancy" },
                {"max_results", "10" },
                {"expansions", "attachments.media_keys,author_id" },
                {"user.fields", "id,name,url,username,profile_image_url" },
                {"media.fields","media_key,type,url" },
                {"tweet.fields","id,created_at,text,author_id,attachments,public_metrics,entities,lang" }
            };
            var url = QueryHelpers.AddQueryString("https://api.twitter.com/2/tweets/search/recent", queryParams);
            var result = await client.GetStringAsync
                (url);
            var returnData = JsonSerializer.Deserialize<TwitterSearchDataV2>(result);
            return returnData;
        }
    }
}

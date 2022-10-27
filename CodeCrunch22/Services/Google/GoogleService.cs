using CodeCrunch22.Models.Google;
using CodeCrunch22.Models.Youtube;
using Octokit;

namespace CodeCrunch22.Services.Google
{
    public class GoogleService : IGoogleService
    {
        private readonly string API_KEY;
        private readonly IHttpClientFactory _clientFactory;

        public GoogleService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            API_KEY = configuration["Google:API_KEY"];
            _clientFactory = clientFactory;
        }
        public async Task<GoogleSearchData> GetGoogleSearchDataAsync(string searchString)
        {
           var term = searchString.Equals("") ? "lectures" : searchString;
           var response = await _clientFactory.CreateClient().GetFromJsonAsync<GoogleSearchData>
                ($"https://www.googleapis.com/customsearch/v1?key={API_KEY}&cx=11da9b80b12ef4a20&q={term}#");
           return response;
        }
    }
}

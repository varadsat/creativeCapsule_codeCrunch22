using CodeCrunch22.Models.Youtube;

namespace CodeCrunch22.Services.Youtube
{
    public class YoutubeService : IYoutubeService
    {
        private readonly string API_KEY;
        private readonly IHttpClientFactory _clientFactory;

        public YoutubeService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            API_KEY = configuration["YouTube:API_KEY"];
            _clientFactory = clientFactory;
        }
        public async Task<YoutubeSearchData> GetSearchData(string searchString, string? pageToken="")
        {
            var response = await _clientFactory.CreateClient().GetFromJsonAsync<YoutubeSearchData>
                ($"https://www.googleapis.com/youtube/v3/search?&part=snippet&type=video&pageToken={pageToken}&key={API_KEY}&regionCode=IN&q={searchString}");
            return response;
        }
    }
}

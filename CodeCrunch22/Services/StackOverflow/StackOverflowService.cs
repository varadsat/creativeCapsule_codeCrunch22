using CodeCrunch22.Models;
using CodeCrunch22.Models.StackoverFlow;

namespace CodeCrunch22.Services.StackOverflow
{
    public class StackOverflowService : IStackOverflowService
    {
        private readonly IHttpClientFactory _clientFactory;

        public StackOverflowService( IHttpClientFactory clientFactory)
        {   
            _clientFactory = clientFactory;
        }

        public async Task<StackOverflowSearchData> GetSearchDataByStringAsync(string searchString)
        {
            var result =  await _clientFactory.CreateClient("SOClient").GetFromJsonAsync<StackOverflowSearchData>
                ($"https://api.stackexchange.com/2.3/search/advanced?site=stackoverflow&order=desc&sort=relevance&filter=!6VvPDzO8siz87&pagesize=10&q={searchString}");
            return result;
        }

        public async Task<StackOverflowSearchData> GetSearchDataByTagsAsync(string tags)
        {
            var formattedTags = tags.Replace(',',';');
            return await _clientFactory.CreateClient("SOClient").GetFromJsonAsync<StackOverflowSearchData>
                ($"https://api.stackexchange.com/2.3/questions?site=stackoverflow&order=desc&sort=activity&filter=!6VvPDzO8siz87&pagesize=10&tagged={formattedTags}");
        }
    }
}

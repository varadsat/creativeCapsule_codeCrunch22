using CodeCrunch22.Models.Youtube;

namespace CodeCrunch22.Services.Youtube
{
    public interface IYoutubeService
    {
        Task<YoutubeSearchData> GetSearchData(string searchString, string? pageToken);
    }
}

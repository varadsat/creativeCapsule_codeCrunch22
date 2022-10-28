using CodeCrunch22.Models.Twitter;
using CodeCrunch22.Models.Twitter.V2;

namespace CodeCrunch22.Services.Twitter
{
    public interface ITwitterService
    {
        Task<TwitterSearchData> GetTwitterSearchDataAsync(string searchString);
        Task<TwitterSearchDataV2> GetTwitterSearchDataV2Async(string searchString);
    }
}

using CodeCrunch22.Models.Twitter;

namespace CodeCrunch22.Services.Twitter
{
    public interface ITwitterService
    {
        Task<TwitterSearchData> GetTwitterSearchDataAsync(string searchString);
    }
}

using CodeCrunch22.Models.Google;

namespace CodeCrunch22.Services.Google
{
    public interface IGoogleService
    {
        Task<GoogleSearchData> GetGoogleSearchDataAsync(string searchString);
    }
}

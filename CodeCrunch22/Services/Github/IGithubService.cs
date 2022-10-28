using CodeCrunch22.Models.Github;

namespace CodeCrunch22.Services.Github
{
    public interface IGithubService
    {
        Task<GithubSearchData> GetSearchData2Async(string searchString);
    }
}

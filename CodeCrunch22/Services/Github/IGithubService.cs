using CodeCrunch22.Models.Github;

namespace CodeCrunch22.Services.Github
{
    public interface IGithubService
    {
        Task<Octokit.SearchRepositoryResult> GetSearchData(string searchString);
        Task<GithubSearchData> GetSearchData2Async(string searchString);
    }
}

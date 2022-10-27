using CodeCrunch22.Models.StackoverFlow;

namespace CodeCrunch22.Services.StackOverflow
{
    public interface IStackOverflowService
    {
        Task<StackOverflowSearchData> GetSearchDataByTagsAsync(string tags);
        Task<StackOverflowSearchData> GetSearchDataByStringAsync(string searchString);
    }
}

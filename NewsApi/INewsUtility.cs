using NewsApi.Model;

namespace NewsApi
{
    public interface INewsUtility
    {
        Task<NewsModel> GetStoryAsync(int storyId);
    }
}



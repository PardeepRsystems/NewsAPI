using NewsApi.Model;

namespace NewsApi.IRepository
{
    public interface INewsRepository
    {
        Task<HttpResponseMessage> NewestStoriesAsync();
        Task<HttpResponseMessage> GetStoryByIdAsync(int id);
    }
}

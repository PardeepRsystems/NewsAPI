using Microsoft.Extensions.Caching.Distributed;
using NewsApi.IRepository;
using NewsApi.Model;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace NewsApi
{
    public class NewsUtility : INewsUtility
    {
        private readonly IMemoryCache _cache;
        private readonly INewsRepository _newsRepository;

        public NewsUtility(IMemoryCache cache, INewsRepository newsRepository)
        {
            _cache = cache;
            _newsRepository = newsRepository;
        }

        public async Task<NewsModel> GetStoryAsync(int storyId)
        {
            return await _cache.GetOrCreateAsync(storyId,
                async cacheEntry =>
                {
                    NewsModel story = new NewsModel();

                    var response = await _newsRepository.GetStoryByIdAsync(storyId);
                    if (response.IsSuccessStatusCode)
                    {
                        var storyResponse = response.Content.ReadAsStringAsync().Result;
                        story = JsonConvert.DeserializeObject<NewsModel>(storyResponse);
                    }

                    return story;
                });
        }
    }
}

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


        /// <summary>
        /// Get stroies from Cache or API
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns> cached or fresh news stories</returns>
        #pragma warning disable CS8603
        public async Task<NewsModel> GetStoryAsync(int storyId)
        {
            try
            {
                var cachedStory = await _cache.GetOrCreateAsync<NewsModel>(storyId, async cacheEntry =>
                {
                    var response = await _newsRepository.GetStoryByIdAsync(storyId);
                    if (response.IsSuccessStatusCode)
                    {
                        var storyResponse = await response.Content.ReadAsStringAsync();
                        var newsModel = JsonConvert.DeserializeObject<NewsModel>(storyResponse);
                        cacheEntry.SetValue(newsModel);                       
                        return newsModel;
                    }
                    else
                    {
                        return new NewsModel();
                    }
                });

                return cachedStory;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

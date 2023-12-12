using NewsApi.IRepository;
using NewsApi.Model;

namespace NewsApi.Repository
{
    public class NewsRepository : INewsRepository
    {

        private static HttpClient client = new HttpClient();       


        /// <summary>
        /// get All Ids for latest news stories
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> NewestStoriesAsync()
        {
            return await client.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
        }

        /// <summary>
        /// Fetch all news stories by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List news stories</returns>
        public async Task<HttpResponseMessage> GetStoryByIdAsync(int id)
        {
            return await client.GetAsync(string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json", id));
        }
      

        
    }
}

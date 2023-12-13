// Susing Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
using NewsApi.IRepository;
using NewsApi.Model;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {       
        private INewsRepository  _newsRepository;
        private readonly INewsUtility _newsUtility;
        
        public NewsController(INewsUtility newsUtility, INewsRepository newsRepository)
        {
            _newsUtility = newsUtility; 
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Help to get all latest news stories
        /// </summary>
        /// <returns> List of latest stories</returns>

        [HttpGet]
        [Route("getAllStroies")]
        public async Task<List<NewsModel>> Stories()
        {
            List<NewsModel> stories = new List<NewsModel>();
            try
            {
                var response = await _newsRepository.NewestStoriesAsync();


                if (response.IsSuccessStatusCode)
                {
                    var storiesResponse = response.Content.ReadAsStringAsync().Result;
                    var storiesIds = JsonConvert.DeserializeObject<List<int>>(storiesResponse);

                    var jobs = storiesIds?.Select(_newsUtility.GetStoryAsync);
                    stories = (await Task.WhenAll(jobs)).ToList();

                    
                }
                return stories;
            }
            catch(Exception) {
                throw;
            }
           
        }

       

    }
}

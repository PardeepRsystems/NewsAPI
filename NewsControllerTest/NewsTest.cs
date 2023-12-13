using NewsApi.Controllers;
using NewsApi.IRepository;
using NewsApi.Model;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using NewsApi;

namespace NewsControllerTest
{
    [TestClass]
    public class NewsTest
    {
        [TestMethod]
        public void GetAllNews_ShouldReturnAllNews()
        {
            var mockNewsRepository = new Mock<INewsRepository>();
            var mockNewsCache = new Mock<IMemoryCache>();
            var mockNewsUtility = new Mock<INewsUtility>();          

            mockNewsRepository.Setup(x => x.NewestStoriesAsync()).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[38544729, 38531759, 38579899]")
            });

            var newsList = new List<NewsModel>
        {
            new NewsModel { Id = 1, Title = "News 1", Url = "Content 1" },
            new NewsModel { Id = 2, Title = "News 2", Url = "Content 2" }
        };


            mockNewsRepository.Setup(x => x.GetStoryByIdAsync(It.IsAny<int>())).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[Id = 2, Title = \"News 2\", Url = \"Content 2\"]")
            });

            var newsController = new NewsController(mockNewsUtility.Object, mockNewsRepository.Object);

            var result = newsController.Stories();
            Assert.IsNotNull(result);
        }

    }

}

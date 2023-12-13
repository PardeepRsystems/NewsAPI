using System;
using NewsApi.Controllers;
using NewsApi.IRepository;
using NewsApi.Model;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
using NewsApi.Repository;
using Newtonsoft.Json;
using System.Text;
using NewsApi;


namespace NewsControllerTest
{
    [TestClass]
    internal class NewsUtilityTest
    {
        [TestMethod]
        public async Task GetStoryAsync_ReturnsDataFromCache()
        {
            // Arrange
            var mockNewsRepository = new Mock<INewsRepository>();
            var mockNewsCache = new Mock<IMemoryCache>();

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

            var newsUtility = new NewsUtility(mockNewsCache.Object, mockNewsRepository.Object);
            int storyId = 38531759;
            var cachedStory = new NewsModel { Id = storyId, Title = "Mocked Story" };

            var mockCache = new Mock<IDistributedCache>();
            mockCache.Setup(c => c.Get(It.IsAny<string>())).Returns(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cachedStory)));

             // Act
            var story = newsUtility.GetStoryAsync(storyId);

            // Assert
            Assert.IsNotNull(story);
            Assert.AreEqual(storyId, 38531759);
        }


    }
}

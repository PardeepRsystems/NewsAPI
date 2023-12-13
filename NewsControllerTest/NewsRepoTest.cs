using Moq;
using NewsApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewsControllerTest
{

    [TestClass]
    public class NewsRepoTest
    {
        [TestMethod]
        public async Task NewestStoriesAsync_ReturnsHttpResponseMessage()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);         

            var newsRepository = new NewsRepository();
         
            // Act
            var response = await newsRepository.NewestStoriesAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task NewestStoriesAsyncById_ReturnsHttpResponseMessage()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var newsRepository = new NewsRepository();

            // Act
            var response = await newsRepository.GetStoryByIdAsync(38544729);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

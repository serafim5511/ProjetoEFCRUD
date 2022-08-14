using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestIntegration.Controller
{
    public class TestUsuarioIntegration : TestIntegration
    {
        private const string _endpoint = "UsuarioListSchema";

        [Fact]
        public async Task GetAllAsync_WhenJsonIsVerifiedBySchema_ShouldReturnTrue()
        {
            //Arrange
            string url = $"{ApiRoutes.ProductsServices.GetUsuarioAsync}";

            //Act
            var response = await TestClient.GetAsync(url);
            var isJsonValid = await ValidateResponse_WithErrorMessages(response, _endpoint);
            var errorMessage = string.Join("\r\n", new List<string>(isJsonValid.Item2.ToList()));

            //Assert
            Assert.True(isJsonValid.Item1, errorMessage);
        }
    }
}

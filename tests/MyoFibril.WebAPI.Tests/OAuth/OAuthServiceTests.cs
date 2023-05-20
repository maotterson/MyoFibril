using System.Threading.Tasks;
using Moq;
using MyoFibril.Contracts.Strava.Responses.OAuth;
using MyoFibril.WebAPI.OAuth;
using MyoFibril.WebAPI.OAuth.Interfaces;
using Xunit;

namespace MyoFibril.WebAPI.Tests.OAuth
{
    public class OAuthServiceTests
    {
        [Fact]
        public async Task GetAccessToken_WithCachedAccessToken_ShouldReturnAccessToken()
        {
            // Arrange
            var tokenCacheMock = new Mock<ITokenCache>();
            tokenCacheMock.Setup(t => t.GetAccessToken()).Returns("cached-access-token");

            var tokenRefreshServiceMock = new Mock<ITokenRefreshService>();

            var oauthService = new OAuthService(tokenRefreshServiceMock.Object, tokenCacheMock.Object);

            // Act
            var accessToken = await oauthService.GetAccessToken();

            // Assert
            Assert.Equal("cached-access-token", accessToken);
            tokenRefreshServiceMock.Verify(t => t.RefreshAccessToken(), Times.Never);
        }

        [Fact]
        public async Task GetAccessToken_WithoutCachedAccessToken_ShouldRefreshAccessToken()
        {
            // Arrange
            var tokenCacheMock = new Mock<ITokenCache>();
            tokenCacheMock.Setup(t => t.GetAccessToken()).Returns(string.Empty);

            var tokenResponse = new NewAccessTokenResponse
            {
                AccessToken = "new-access-token"
            };

            var tokenRefreshServiceMock = new Mock<ITokenRefreshService>();
            tokenRefreshServiceMock.Setup(t => t.RefreshAccessToken()).ReturnsAsync(tokenResponse);

            var oauthService = new OAuthService(tokenRefreshServiceMock.Object, tokenCacheMock.Object);

            // Act
            var accessToken = await oauthService.GetAccessToken();

            // Assert
            Assert.Equal("new-access-token", accessToken);
            tokenCacheMock.Verify(t => t.SetAccessToken("new-access-token"), Times.Once);
        }
    }
}
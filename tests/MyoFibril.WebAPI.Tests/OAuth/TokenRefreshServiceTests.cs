using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using MyoFibril.Contracts.Strava.Responses.OAuth;
using MyoFibril.WebAPI.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyoFibril.WebAPI.Tests.OAuth;
public class TokenRefreshServiceTests
{
    [Fact]
    public async Task RefreshAccessToken_ShouldReturnNewAccessToken()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var configurationMock = new Mock<IConfiguration>();

        var httpClientMock = new Mock<HttpClient>();

        var responseContent = JsonSerializer.Serialize(new NewAccessTokenResponse
        {
            AccessToken = "new-access-token"
        });

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        };

        httpClientMock
            .Setup(c => c.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponseMessage);

        httpClientFactoryMock
            .Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(httpClientMock.Object);

        configurationMock.Setup(c => c["StravaApp:RequestUri"]).Returns("https://example.com");
        configurationMock.Setup(c => c["StravaApp:ClientId"]).Returns("client-id");
        configurationMock.Setup(c => c["StravaApp:ClientSecret"]).Returns("client-secret");
        configurationMock.Setup(c => c["StravaApp:RefreshToken"]).Returns("refresh-token");

        var tokenRefreshService = new TokenRefreshService(httpClientFactoryMock.Object, configurationMock.Object);

        // Act
        var result = await tokenRefreshService.RefreshAccessToken();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("new-access-token", result.AccessToken);
    }
}

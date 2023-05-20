using Moq;
using MyoFibril.WebAPI.OAuth;
using MyoFibril.WebAPI.OAuth.Interfaces;

namespace MyoFibril.WebAPI.Tests.OAuth;

public class AuthHeaderHandlerTests
{
    [Fact]
    public async Task SendAsync_ShouldSetAuthorizationHeader()
    {
        // Arrange
        var token = "test-token";
        var oauthServiceMock = new Mock<IOAuthService>();
        oauthServiceMock.Setup(o => o.GetAccessToken()).ReturnsAsync(token);

        var innerHandler = new HttpClientHandler();
        var httpClient = new HttpClient(new TestAuthHeaderHandler(oauthServiceMock.Object, innerHandler));

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        Assert.Equal("Bearer " + token, request.Headers.Authorization!.ToString());
    }
}
public class TestAuthHeaderHandler : AuthHeaderHandler
{
    public TestAuthHeaderHandler(IOAuthService oauthService, HttpMessageHandler innerHandler) : base(oauthService)
    {
        InnerHandler = innerHandler;
    }

    public Task<HttpResponseMessage> TestSendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return SendAsync(request, cancellationToken);
    }
}

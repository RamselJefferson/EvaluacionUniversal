using System.Net;
using System.Text;
using System.Text.Json;
using Core.Application.Helper;
using Infrastructure.Persistance.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

public class JsonPlaceHolderServiceTests
{
    private readonly JsonPlaceHolderService _service;
    private readonly Mock<HttpMessageHandler> _mockHttpHandler;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public JsonPlaceHolderServiceTests()
    {
        _mockHttpHandler = new Mock<HttpMessageHandler>();

        _httpClient = new HttpClient(_mockHttpHandler.Object)
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com") // Simulación
        };

        var inMemorySettings = new Dictionary<string, string>
        {
            { "jsonPlaceHolder:baseUrl", "https://jsonplaceholder.typicode.com" }
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _service = new JsonPlaceHolderService(_httpClient, _configuration);
    }

    [Fact]
    public async Task GetData_ShouldReturnListOfPosts()
    {
        var fakeResponse = new List<JsonPlaceHolderModel>
        {
            new JsonPlaceHolderModel { UserId = 1, Id = 1, Title = "Test Title", Body = "Test Body" }
        };

        var json = JsonSerializer.Serialize(fakeResponse);
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        _mockHttpHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponse);

        var result = await _service.GetData();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Title", result[0].Title);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedPost()
    {
        var request = new JsonPlaceHolderModel { UserId = 1, Title = "New Post", Body = "Content" };

        var json = JsonSerializer.Serialize(request);
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        _mockHttpHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponse);

        var result = await _service.Create(request);

        Assert.NotNull(result);
        Assert.Equal("New Post", result.Title);
    }

    [Fact]
    public async Task GetData_ShouldThrowException_WhenApiFails()
    {
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        _mockHttpHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponse);

        await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetData());
    }
}

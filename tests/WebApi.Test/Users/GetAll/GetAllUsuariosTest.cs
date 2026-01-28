using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Users.GetAll;

public class GetAllUsuariosTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios";

    private readonly string _token;

    public GetAllUsuariosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("usuarios").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
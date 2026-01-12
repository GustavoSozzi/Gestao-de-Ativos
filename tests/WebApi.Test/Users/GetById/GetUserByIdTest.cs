using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Users.GetById;

public class GetUserByIdTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios";

    private readonly string _token;
    private readonly long _usuarioId;

    public GetUserByIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _usuarioId = webApplicationFactory.User_Team_Member.GetUsuarioId();
    }
    
    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{METHOD}/{_usuarioId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("p_nome").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("sobrenome").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("matricula").GetInt64().Should().BeGreaterThan(0);
        response.RootElement.GetProperty("departamento").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("cargo").GetString().Should().NotBeNullOrWhiteSpace();
    }
}
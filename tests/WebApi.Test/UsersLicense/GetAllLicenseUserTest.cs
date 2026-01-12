using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.UsersLicense;

public class GetAllAtivosTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios";
    private readonly string _token;
    private readonly long _usuarioId;
    
    public GetAllAtivosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _usuarioId = webApplicationFactory.User_Team_Member.GetUsuarioId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{METHOD}/{_usuarioId}/licencas", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.EnumerateArray().Should().NotBeNull();
    }
}
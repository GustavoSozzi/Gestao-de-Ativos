using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Users.Profile;

public class GetUserProfileTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios/profile";
    
    private readonly string _token;
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly long _userMatricula;

    public GetUserProfileTest(CustomWebApplicationFactory WebApplicationFactory) : base(
        WebApplicationFactory)
    {
        _token = WebApplicationFactory.User_Team_Member.GetToken();
        _firstName = WebApplicationFactory.User_Team_Member.GetFirstName();
        _lastName = WebApplicationFactory.User_Team_Member.GetLastName();
        _userMatricula = WebApplicationFactory.User_Team_Member.GetMatricula();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(METHOD, _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("p_nome").GetString().Should().Be(_firstName);
        response.RootElement.GetProperty("sobrenome").GetString().Should().Be(_lastName);
        response.RootElement.GetProperty("matricula").GetInt64().Should().Be(_userMatricula);
        
    }
}
using System.Net;
using FluentAssertions;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios";
    
    private readonly string _token;
    
    public DeleteUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
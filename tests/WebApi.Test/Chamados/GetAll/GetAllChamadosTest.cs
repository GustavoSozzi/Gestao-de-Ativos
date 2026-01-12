using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Chamados.GetAll;

public class GetAllChamadosTest : AtivosClassFixture
{
    private const string METHOD = "api/Chamados";

    private readonly string _token;

    public GetAllChamadosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Admin.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("chamados").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
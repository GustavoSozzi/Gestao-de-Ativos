using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Ativos.GetById;

public class GetAtivoByIdTest : AtivosClassFixture
{
    private const string METHOD = "api/Ativos";

    private readonly string _token;
    private readonly long _ativoId;

    public GetAtivoByIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _ativoId = webApplicationFactory.Ativos.GetAtivoId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{METHOD}/{_ativoId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("modelo").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("serialNumber").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("codInventario").GetInt64().Should().BeGreaterThan(0);
        response.RootElement.GetProperty("tipo").GetString().Should().NotBeNullOrWhiteSpace();
    }
}
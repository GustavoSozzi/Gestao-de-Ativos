using System.Net;
using System.Text.Json;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Ativos.Delete;

public class DeleteAtivoTest : AtivosClassFixture
{
    private const string METHOD = "api/Ativos";

    private readonly string _token;
    private readonly long _ativoId;

    public DeleteAtivoTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _ativoId = webApplicationFactory.Ativos.GetAtivoId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: $"{METHOD}/{_ativoId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        result = await DoGet(requestUri: $"{METHOD}/{_ativoId}", token: _token);
        
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Ativo_Not_Found(string culture)
    {
        var result = await DoDelete(requestUri: $"{METHOD}/10000", token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals("NOT FOUND"));
    }
    
}
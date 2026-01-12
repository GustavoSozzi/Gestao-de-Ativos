using System.Globalization;
using System.Net;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Requests.Update;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Ativos.Update;

public class UpdateAtivosTest : AtivosClassFixture
{
    private const string METHOD = "api/Ativos";

    private readonly string _token;
    private readonly long _ativoId;

    public UpdateAtivosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _ativoId = webApplicationFactory.Ativos_MemberTeam.GetAtivoId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateAtivosJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/{_ativoId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestUpdateAtivosJsonBuilder.Build();
        request.Nome = string.Empty;
        
        var result = await DoPut(requestUri: $"{METHOD}/{_ativoId}", request: request, token: _token, culture: culture);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
    
    [Fact]
    public async Task Error_Ativo_Not_Found()
    {
        var request = RequestUpdateAtivosJsonBuilder.Build();
        
        var result = await DoPut(requestUri: $"{METHOD}/10000", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals("NOT FOUND"));
    }
    
}
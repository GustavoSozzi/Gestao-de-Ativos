using System.Globalization;
using System.Net;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Chamados.Update;

public class UpdateChamadosTest : AtivosClassFixture
{
    private const string METHOD = "api/Chamados";

    private readonly string _token;
    private readonly long _chamadoId;

    public UpdateChamadosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _chamadoId = webApplicationFactory.Chamados.GetChamadoId();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterChamadosJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/{_chamadoId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Titulo = string.Empty;
        
        var result = await DoPut(requestUri: $"{METHOD}/{_chamadoId}", request: request, token: _token, culture: culture);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("FIELD_REQUIRED", new System.Globalization.CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
    
}
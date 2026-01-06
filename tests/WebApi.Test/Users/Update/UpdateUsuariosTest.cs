using System.Globalization;
using System.Net;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using CommonTestUtilities.Requests.Update;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Ativos.Update;

public class UpdateUsuariosTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios";

    private readonly string _token;
    private readonly long _usuarioId;

    public UpdateUsuariosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _usuarioId = webApplicationFactory.User_Team_Member.GetUsuarioId();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/{_usuarioId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.P_nome = string.Empty;
        
        var result = await DoPut(requestUri: $"{METHOD}/{_usuarioId}", request: request, token: _token, culture: culture);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        
        var result = await DoPut(requestUri: $"{METHOD}/10000", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals("NOT FOUND"));
    }
}
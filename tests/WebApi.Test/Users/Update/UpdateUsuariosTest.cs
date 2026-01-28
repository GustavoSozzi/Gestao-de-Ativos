using System.Globalization;
using System.Net;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Entities;
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
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateUserJsonBuilder.Build();

        var result = await DoPut(requestUri: METHOD, request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        request.P_Nome = string.Empty;
        
        var result = await DoPut(requestUri: METHOD, request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));
        
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
        
}
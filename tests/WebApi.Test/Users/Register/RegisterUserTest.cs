using System.Globalization;
using System.Net;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios";
    private readonly string _token;

    public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();

        var result = await DoPost(requestUri:METHOD, request:request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("p_nome").GetString().Should().Be(request.P_nome);
        response.RootElement.GetProperty("sobrenome").GetString().Should().Be(request.Sobrenome);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.P_nome = string.Empty;

        var result = await DoPost(requestUri:METHOD, request: request, token: _token, culture: culture);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new System.Globalization.CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
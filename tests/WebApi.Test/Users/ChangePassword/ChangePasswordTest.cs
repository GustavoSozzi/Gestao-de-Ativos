using System.Globalization;
using System.Net;
using System.Text.Json;
using Ativos.Communication.Requests;
using Ativos.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Users.ChangePassword;

public class ChangePasswordTest : AtivosClassFixture
{
    private const string METHOD = "api/Usuarios/change-password";

    private readonly string _token;
    private readonly string _password;
    private readonly long _matricula;
    
    public ChangePasswordTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _password = webApplicationFactory.User_Team_Member.GetPassword();
        _matricula = webApplicationFactory.User_Team_Member.GetMatricula();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = _password;
        
        var response = await DoPut(METHOD, request, _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var loginRequest = new RequestLoginJson
        {
            Matricula = _matricula,
            Password = _password,
        };
        
        response = await DoPost("api/Login", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        
        loginRequest.Password = request.NewPassword;
        
        response = await DoPost("api/Login", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Password_Different_Current_Password(string culture)
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        
        var response = await DoPut(METHOD, request, token: _token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_PASSWORD", new CultureInfo(culture));
        
        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}
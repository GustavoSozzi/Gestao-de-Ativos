using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Ativos.Communication.Requests;
using Ativos.Exception;
using CommonTestUtilities.Requests.Login;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : AtivosClassFixture
{
    private const string METHOD = "api/login";
    
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string _password;
    private readonly long _matricula;

    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _firstName = webApplicationFactory.User_Team_Member.GetFirstName();
        _lastName = webApplicationFactory.User_Team_Member.GetLastName();
        _password = webApplicationFactory.User_Team_Member.GetPassword();
        _matricula = webApplicationFactory.User_Team_Member.GetMatricula();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson { Matricula = _matricula, Password = _password };

        var response = await DoPost(requestUri: METHOD, request: request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responsebody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responsebody);

        responseData.RootElement.GetProperty("p_nome").GetString().Should().Be(_firstName);
        responseData.RootElement.GetProperty("sobrenome").GetString().Should().Be(_lastName);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();    
    }

    [Fact]
    public async Task Error_Login_Invalid()
    {
        var request = RequestLoginJsonBuilder.Build();

        var response = await DoPost(requestUri: METHOD, request: request, culture: "en");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        
        var responseBody =  await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        ResourceErrorMessages.Culture = new System.Globalization.CultureInfo("en");
        var expectedMessage = ResourceErrorMessages.MATRICULA_OU_SENHA_INVALIDA;
        
        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}
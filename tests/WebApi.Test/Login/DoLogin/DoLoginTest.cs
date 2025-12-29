using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Ativos.Communication.Requests;
using FluentAssertions;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/login";
    
    private readonly HttpClient _httpClient;
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string _password;
    private readonly long _matricula;

    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _firstName = webApplicationFactory.GetFirstName();
        _lastName = webApplicationFactory.GetLastName();
        _password = webApplicationFactory.GetPassword();
        _matricula = webApplicationFactory.GetMatricula();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson { Matricula = _matricula, Password = _password };

        var response = await _httpClient.PostAsJsonAsync(METHOD, request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responsebody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responsebody);

        responseData.RootElement.GetProperty("p_nome").GetString().Should().Be(_firstName);
        responseData.RootElement.GetProperty("sobrenome").GetString().Should().Be(_lastName);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();    
    }
}
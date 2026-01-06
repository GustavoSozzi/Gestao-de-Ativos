using System.Net;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using WebApi.Test.inlineData;

namespace WebApi.Test.Ativos.Register;

public class RegisterAtivosTest : AtivosClassFixture
{
    private const string METHOD = "api/Ativos";
    
    private readonly HttpClient _httpClient;
    private readonly string _token;

    public RegisterAtivosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();

        var result = await DoPost(requestUri: METHOD, request: request, token : _token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("nome").GetString().Should().Be(request.Nome);
        response.RootElement.GetProperty("modelo").GetString().Should().Be(request.Modelo);
        response.RootElement.GetProperty("serialNumber").GetString().Should().Be(request.SerialNumber);
    }
    
    [Fact]
    public async Task Error_Empty_Name()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Nome = string.Empty;
        
        var result = await DoPost(requestUri: METHOD, request: request, token: _token);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Model(string culture)
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Modelo = string.Empty;
        
        var result = await DoPost(requestUri: METHOD, request: request, token: _token, culture: culture);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("FIELD_REQUIRED", new System.Globalization.CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
    
    [Fact]
    public async Task Error_Empty_SerialNumber()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.SerialNumber = string.Empty;
        
        var result = await DoPost(requestUri: METHOD, request: request, token: _token);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    
}
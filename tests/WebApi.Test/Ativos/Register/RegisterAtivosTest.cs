using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Ativos.Exception;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace WebApi.Test.Ativos.Register;

public class RegisterAtivosTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Ativos";
    
    private readonly HttpClient _httpClient;

    public RegisterAtivosTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

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
        
        var result = await _httpClient.PostAsJsonAsync(METHOD, request);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }
    
    [Fact]
    public async Task Error_Empty_Model()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Modelo = string.Empty;
        
        var result = await _httpClient.PostAsJsonAsync(METHOD, request);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public async Task Error_Empty_SerialNumber()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.SerialNumber = string.Empty;
        
        var result = await _httpClient.PostAsJsonAsync(METHOD, request);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    
}
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;

namespace WebApi.Test.Chamados.Register;

public class RegisterChamadosTest : AtivosClassFixture
{
    private const string METHOD = "api/Chamados";
    
    private readonly HttpClient _httpClient;
    private readonly string _token;
    private readonly long _IdAtivo;

    public RegisterChamadosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _IdAtivo = webApplicationFactory.Ativos.GetAtivoId();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Id_Ativo = _IdAtivo;

        var result = await DoPost(requestUri: METHOD, request: request, token : _token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.GetProperty("id_Chamado").GetInt32().Should().BeGreaterThan(0);
    }
}
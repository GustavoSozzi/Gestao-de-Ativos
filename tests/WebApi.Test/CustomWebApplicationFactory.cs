using Ativos.Domain.Entities;
using Ativos.Domain.Security.Cryptography;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private Usuario _user;
    private Chamado _chamado;
    private Ativo _ativo;
    private string _password;
    
    public string GetFirstName() => _user.P_nome;
    public string GetLastName() => _user.Sobrenome;
    public long GetMatricula() => _user.Matricula;
    public string GetPassword() => _password;
    public long GetAtivo() => _chamado.id_Ativo;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider(); 
                
                services.AddDbContext<AtivosDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AtivosDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                
                StartDatabase(dbContext, passwordEncripter);
            });
    }

    private void StartDatabase(AtivosDbContext dbContext, IPasswordEncripter passwordEncripter)
    {
        _user = UserBuilder.Build();
        _chamado = ChamadosBuilder.Build();
        _ativo = AtivosBuilder.Build();
        
        _password = _user.Password;
        
        _user.Password = passwordEncripter.Encrypt(_user.Password);

        dbContext.Usuario.Add(_user);
        dbContext.Chamados.Add(_chamado);
        dbContext.Ativos.Add(_ativo);
        
        dbContext.SaveChanges();
    }
}
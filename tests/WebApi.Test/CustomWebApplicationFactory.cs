using Ativos.Domain.Entities;
using Ativos.Domain.Security.Cryptography;
using Ativos.Domain.Security.Tokens;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public AtivosIdentityManager Ativos { get; private set; }
    public ChamadosIdentityManager Chamados { get; private set; }
    public UserIdentityManager User_Team_Member { get; private set; }
    public UserIdentityManager User_Admin { get; private set; }

    
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
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();
                
                StartDatabase(dbContext, passwordEncripter, accessTokenGenerator);
            });
    }

    private void StartDatabase(AtivosDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = AddUsersTeamMember(dbContext, passwordEncripter, accessTokenGenerator);
        var localizacao = AddLocalizacao(dbContext);
        AddAtivos(dbContext, user, localizacao);
        AddChamados(dbContext);
        
        dbContext.SaveChanges();
    }

    private Usuario AddUsersTeamMember(AtivosDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
       var user = UserBuilder.Build();
       var password = user.Password;

        user.Password = passwordEncripter.Encrypt(user.Password);
        
        dbContext.Usuario.Add(user);

        var token = accessTokenGenerator.Generate(user);
        User_Team_Member = new UserIdentityManager(user, password, token);

        return user;
    }

    private Localizacao AddLocalizacao(AtivosDbContext dbContext)
    {
        var localizacao = LocalizacaoBuilder.Build();
        
        dbContext.Localizacao.Add(localizacao);
        
        return localizacao;
    }

    private void AddAtivos(AtivosDbContext dbContext, Usuario usuario, Localizacao localizacao)
    {
        var ativo = AtivosBuilder.Build(usuario);
        ativo.id_localizacao = localizacao.Id_Localizacao;
        
        dbContext.Ativos.Add(ativo);

        Ativos = new AtivosIdentityManager(ativo);
    }

    private void AddChamados(AtivosDbContext dbContext)
    {
        var chamado = ChamadosBuilder.Build();

        dbContext.Chamados.Add(chamado);

        Chamados =  new ChamadosIdentityManager(chamado);
    }
}
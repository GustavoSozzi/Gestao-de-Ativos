using Ativos.Domain.Entities;
using Ativos.Domain.Enums;
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
    public AtivosIdentityManager Ativos_MemberTeam { get; private set; }
    public AtivosIdentityManager Ativos_Admin { get; private set; }
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
        var userTeamMember = AddUsersTeamMember(dbContext, passwordEncripter, accessTokenGenerator);
        
        var localizacao = AddLocalizacao(dbContext);
        var ativoMemberTeam = AddAtivos(dbContext, userTeamMember, localizacao, ativoId: 20);
        Ativos_MemberTeam = new AtivosIdentityManager(ativoMemberTeam);
        
        var licenca = AddLicencas(dbContext);

        var userAdmin = AddUsersAdmin(dbContext, passwordEncripter, accessTokenGenerator);
        var ativosAdmin = AddAtivos(dbContext, userAdmin, localizacao, ativoId: 19);
        Ativos_Admin = new AtivosIdentityManager(ativosAdmin);
        
        AddChamados(dbContext, ativosAdmin);
        
        userTeamMember.licencas.Add(licenca);
        
        dbContext.SaveChanges();
    }

    private Usuario AddUsersTeamMember(AtivosDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id_usuario = 8;
        
        var password = user.Password;
        user.Password = passwordEncripter.Encrypt(user.Password);
        
        dbContext.Usuario.Add(user);

        var token = accessTokenGenerator.Generate(user);
        User_Team_Member = new UserIdentityManager(user, password, token);

        return user;
    }
    
    private Usuario AddUsersAdmin(AtivosDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.ADMIN);
        user.Id_usuario = 7;
        
        var password = user.Password;
        user.Password = passwordEncripter.Encrypt(user.Password);
        
        dbContext.Usuario.Add(user);

        var token = accessTokenGenerator.Generate(user);
        User_Admin = new UserIdentityManager(user, password, token);

        return user;
    }

    private Licenca AddLicencas(AtivosDbContext dbContext)
    {
        var licencas = LicenceBuilder.Build();

        dbContext.Licencas.Add(licencas);

        return licencas;
    }

    private Localizacao AddLocalizacao(AtivosDbContext dbContext)
    {
        var localizacao = LocalizacaoBuilder.Build();
        
        dbContext.Localizacao.Add(localizacao);
        
        return localizacao;
    }

    private Ativo AddAtivos(AtivosDbContext dbContext, Usuario usuario, Localizacao localizacao, long ativoId)
    {
        var ativo = AtivosBuilder.Build(usuario);
        ativo.Id_ativo = ativoId;
        ativo.id_usuario = usuario.Id_usuario;
        ativo.id_localizacao = localizacao.Id_Localizacao;
        
        dbContext.Ativos.Add(ativo);
        
        return ativo;
    }

    private void AddChamados(AtivosDbContext dbContext, Ativo ativo)
    {
        var chamado = ChamadosBuilder.Build(ativo);
        
        dbContext.Chamados.Add(chamado);
        
        Chamados =  new ChamadosIdentityManager(chamado);
    }
}
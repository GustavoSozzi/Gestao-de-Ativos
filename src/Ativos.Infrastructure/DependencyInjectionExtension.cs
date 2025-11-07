using Ativos.Domain;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Domain.Repositories.Contratos;
using Ativos.Domain.Repositories.Licencas;
using Ativos.Domain.Repositories.Localizacao;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ativos.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAtivosWriteOnlyRepository, AtivosRepository>();
        services.AddScoped<IAtivosReadOnlyRepository, AtivosRepository>();
        services.AddScoped<IUsuariosWriteOnlyRepository, UsuariosRepository>();
        services.AddScoped<IChamadosWriteOnlyRepository, ChamadosRepository>();
        services.AddScoped<IContratosWriteOnlyRepository, ContratosRepository>();
        services.AddScoped<ILicencasWriteOnlyRepository, LicencasRepository>();
        services.AddScoped<ILocalizacaoWriteOnlyRepository, LocalizacaoRepository>();
        services.AddScoped<ILocalizacaoReadOnlyRepository, LocalizacaoRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var version = new Version(9, 1, 0);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<AtivosDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
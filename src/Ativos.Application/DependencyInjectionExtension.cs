using Microsoft.Extensions.DependencyInjection;
using Ativos.Application.AutoMapper;
using Ativos.Application.UseCases.Delete.Ativos;
using Ativos.Application.UseCases.GetAll;
using Ativos.Application.UseCases.GetAll.Usuarios;
using Ativos.Application.UseCases.GetById;
using Ativos.Application.UseCases.Login.DoLogin;
using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Application.UseCases.Register.Contratos;
using Ativos.Application.UseCases.Register.Licencas;
using Ativos.Application.UseCases.Register.Localizacao;
using Ativos.Application.UseCases.Register.Usuarios;
using Ativos.Application.UseCases.Update;
using Ativos.Application.UseCases.Update.Usuarios;

namespace Ativos.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services) => services.AddAutoMapper(typeof(AutoMapping));

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterAtivosUseCase, RegisterAtivosUseCase>();
        services.AddScoped<IGetAllAtivosUseCase,  GetAllAtivosUseCase>();
        services.AddScoped<IUpdateAtivosUseCase,  UpdateAtivosUseCase>();
        services.AddScoped<IRegisterUsuariosUseCase, RegisterUsuariosUseCase>();
        services.AddScoped<IGetAllUsuarioUseCase, GetAllUsuarioUseCase>();
        services.AddScoped<IGetUsuarioByIdUseCase, GetUsuarioByIdUseCase>();
        services.AddScoped<IUpdateUsuariosUseCase, UpdateUsuariosUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IRegisterChamadosUseCase, RegisterChamadosUseCase>();
        services.AddScoped<IRegisterContratosUseCase, RegisterContratosUseCase>();
        services.AddScoped<IRegisterLicencasUseCase, RegisterLicencasUseCase>();
        services.AddScoped<IRegisterLocalizacaoUseCase, RegisterLocalizacaoUseCase>();
        services.AddScoped<IGetAllLocalizacaoUseCase, GetAllLocalizacoUseCase>();
        services.AddScoped<IDeleteAtivoUseCase, DeleteAtivoUseCase>();
    }
}
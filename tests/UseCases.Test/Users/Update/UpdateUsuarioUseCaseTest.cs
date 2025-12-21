using Ativos.Application.UseCases.Update.Usuarios;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using CommonTestUtilities.Requests.Update;
using FluentAssertions;

namespace UseCases.Test.Users.Update;

public class UpdateUsuarioUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var usuario = UserBuilder.Build();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        var useCase = CreateUseCase(usuario, usuario.Id_usuario);

        await useCase.Execute(usuario.Id_usuario, request); //nao lancou excecao = passou no teste

        usuario.P_nome.Should().Be(request.P_nome);
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        var usuario = UserBuilder.Build();
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        
        var useCase = CreateUseCase(usuario);

        var act = async () => await useCase.Execute(usuario.Id_usuario, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }
    
    private UpdateUsuariosUseCase CreateUseCase(Usuario usuarios, long? Id_usuario = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateRepository = new UsuariosUpdateOnlyRepositoryBuilder();
        
        if (Id_usuario.HasValue) updateRepository.GetById(usuarios);
        
        return new UpdateUsuariosUseCase(mapper, unitOfWork, updateRepository.Build());
    }
}
using Ativos.Application.UseCases.Update.Usuarios;
using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using CommonTestUtilities.Requests.Update;
using FluentAssertions;
using FluentValidation.Results;

namespace UseCases.Test.Users.Update;

public class UpdateUsuarioUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        
        var useCase = CreateUseCase(loggedUser, loggedUser.Id_usuario);
        
        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        loggedUser.P_nome.Should().Be(request.P_Nome);
        loggedUser.Sobrenome.Should().Be(request.Sobrenome);
        loggedUser.Matricula.Should().Be(request.Matricula);
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        var usuario = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        
        var useCase = CreateUseCase(usuario);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }

    [Fact]
    public async Task Error_Matricula_Already_Exist()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Matricula = 9090;
        
        var useCase = CreateUseCase(loggedUser, Id_usuario: loggedUser.Id_usuario, matricula: request.Matricula);
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Matrícula já existe"));
    }
    
    private UpdateUsuariosUseCase CreateUseCase(Usuario usuarios, long? Id_usuario = null, long matricula = 0)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuarios);
        var updateRepository = new UsuariosUpdateOnlyRepositoryBuilder();
        var readRepository = new UserReadOnlyRepositoryBuilder();
        
        if (Id_usuario.HasValue) updateRepository.WithUser(usuarios);
        if(matricula != 0) readRepository.ExistActiveUserMatricula(matricula);
        
        return new UpdateUsuariosUseCase(unitOfWork, loggedUser, readRepository.Build(), updateRepository.BuildRepository());
    }
}
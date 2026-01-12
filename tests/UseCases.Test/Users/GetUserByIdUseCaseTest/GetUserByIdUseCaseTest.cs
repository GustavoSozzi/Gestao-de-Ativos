using Ativos.Application.UseCases.GetById;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Users.GetUserByIdUseCaseTest;

public class GetUserByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        
        var useCase = CreateUseCase(loggedUser, loggedUser.Id_usuario);

        var result = await useCase.Execute(loggedUser.Id_usuario);

        result.Should().NotBeNull();
        result.P_nome.Should().Be(loggedUser.P_nome);
        result.Sobrenome.Should().Be(loggedUser.Sobrenome);
        result.Cargo.Should().Be(loggedUser.Cargo); 
        result.Departamento.Should().Be(loggedUser.Departamento); 
        result.Matricula.Should().Be(loggedUser.Matricula); 
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        
        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 10000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }
    
    private GetUsuarioByIdUseCase CreateUseCase(Usuario usuario, long? id_usuario = null)
    {
        var repository = new UserReadOnlyRepositoryBuilder().GetById(usuario, id_usuario).Build();
        var mapper = MapperBuilder.Build();

        return new GetUsuarioByIdUseCase(repository, mapper);
    }
}
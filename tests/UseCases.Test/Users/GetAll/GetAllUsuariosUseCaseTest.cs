using Ativos.Application.UseCases.GetAll.Usuarios;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Users.GetAll;

public class GetAllUsuariosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var usuarios = UserBuilder.Collection();
        
        var useCase = CreateUseCase(usuarios);
        
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Usuarios.Should().NotBeNullOrEmpty().And.AllSatisfy(usuario =>
        {
            usuario.Id_usuario.Should().BeGreaterThan(0);
            usuario.P_nome.Should().NotBeNullOrEmpty();
            usuario.Sobrenome.Should().NotBeNullOrEmpty();
            usuario.Matricula.Should().BeGreaterThan(0);
            usuario.Departamento.Should().NotBeNullOrEmpty();
            usuario.Cargo.Should().NotBeNullOrEmpty();
        });
    }

    private GetAllUsuarioUseCase CreateUseCase(List<Usuario> usuarios)
    {
        var repository = new UserReadOnlyRepositoryBuilder().GetAll(usuarios!).Build();
        var mapper = MapperBuilder.Build();
        
        return new GetAllUsuarioUseCase(repository, mapper);
    }
}
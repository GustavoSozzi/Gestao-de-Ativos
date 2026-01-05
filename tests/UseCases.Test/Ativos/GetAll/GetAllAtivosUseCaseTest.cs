using Ativos.Application.UseCases.GetAll;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace WebApi.Test.Ativos.GetAll;

public class GetAllAtivosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var ativos = AtivosBuilder.Collection(loggedUser);
        
        var useCase = CreateUseCase(loggedUser, ativos);
        
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Ativos.Should().NotBeNullOrEmpty().And.AllSatisfy(ativo =>
        {
            ativo.Id_ativo.Should().BeGreaterThan(0);
            ativo.id_localizacao.Should().BeGreaterThan(0);
            ativo.Nome.Should().NotBeNullOrEmpty();
            ativo.Modelo.Should().NotBeNullOrEmpty();
            ativo.SerialNumber.Should().NotBeNullOrEmpty();
            ativo.CodInventario.Should().BeGreaterThan(0);
            ativo.Tipo.Should().NotBeNullOrEmpty();
            ativo.id_usuario.Should().BeGreaterThan(0);
        });
    }

    private GetAllAtivosUseCase CreateUseCase(Usuario usuario, List<Ativo> ativos)
    {
        var repository = new AtivosReadOnlyRepositoryBuilder().GetAll(usuario, ativos).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new GetAllAtivosUseCase(repository, loggedUser, mapper);
    }
}
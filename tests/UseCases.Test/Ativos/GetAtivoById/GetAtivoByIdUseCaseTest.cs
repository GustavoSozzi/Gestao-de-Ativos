using Ativos.Application.UseCases.GetById;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Ativos.GetByIdAtivosUseCaseTest;

public class GetAtivoByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var ativos = AtivosBuilder.Build(loggedUser);
        
        var useCase = CreateUseCase(loggedUser, ativos);

        var result = await useCase.Execute(ativos.Id_ativo);

        result.Should().NotBeNull();
        result.Nome.Should().Be(ativos.Nome);
        result.Modelo.Should().Be(ativos.Modelo);
        result.SerialNumber.Should().Be(ativos.SerialNumber);
        result.CodInventario.Should().Be(ativos.CodInventario);
    }

    [Fact]
    public async Task Error_Ativos_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        
        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }

    private GetAtivoByIdUseCase CreateUseCase(Usuario usuario, Ativo? ativo = null)
    {
        var repository = new AtivosReadOnlyRepositoryBuilder().GetById(usuario, ativo).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);

        return new GetAtivoByIdUseCase(repository, loggedUser, mapper);
    }
}
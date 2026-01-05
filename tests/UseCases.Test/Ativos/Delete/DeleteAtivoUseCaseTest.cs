using Ativos.Application.UseCases.Delete.Ativos;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace WebApi.Test.Ativos.Delete;

public class DeleteAtivoUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var ativos = AtivosBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, ativos);

        var act = async () => await useCase.Execute(ativos.Id_ativo);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Ativo_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        
        var act = async () => await useCase.Execute(id: 10000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }
    
    private DeleteAtivosUseCase CreateUseCase(Usuario usuario, Ativo? ativo = null)
    {
        var repositoryWriteOnly = AtivosWriteOnlyRepositoryBuilder.Build();
        var repositoryReadOnly = new AtivosReadOnlyRepositoryBuilder().GetById(usuario, ativo).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new DeleteAtivosUseCase(repositoryWriteOnly, repositoryReadOnly, unitOfWork, loggedUser);
    }
}
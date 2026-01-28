using Ativos.Application.UseCases.Delete.Usuarios;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Users.Delete;

public class DeleteUserAccountUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }

    private DeleteUsuarioUseCase CreateUseCase(Usuario usuario)
    {
        var repository = UserWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        var unitOfWork = UnitOfWorkBuilder.Build();
        
        return new DeleteUsuarioUseCase(repository, unitOfWork, loggedUser);
    }
}
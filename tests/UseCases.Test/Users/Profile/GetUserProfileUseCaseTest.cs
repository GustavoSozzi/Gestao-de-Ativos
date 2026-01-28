using Ativos.Application.UseCases.Users.Profile;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using FluentAssertions;

namespace UseCases.Test.Users.Profile;

public class GetUserProfileUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.P_nome.Should().Be(user.P_nome);
        result.Sobrenome.Should().Be(user.Sobrenome);
        result.Matricula.Should().Be(user.Matricula);
    }

    private GetUserProfileUseCase CreateUseCase(Usuario usuario)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new GetUserProfileUseCase(loggedUser, mapper);
    }
}
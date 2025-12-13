using Ativos.Application.UseCases.Login.DoLogin;
using Ativos.Domain.Entities;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Login;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Login;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.P_nome.Should().Be(user.P_nome);
        result.Sobrenome.Should().Be(user.Sobrenome);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Use_Not_Found()
    {
        
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        
    }

    private DoLoginUseCase CreateUseCase(Usuario usuario)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByMatricula(usuario).Build(); //chamada encadeada
        var mapper = MapperBuilder.Build();
        
        return new DoLoginUseCase(readRepository, passwordEncripter, tokenGenerator, mapper);
    }
}
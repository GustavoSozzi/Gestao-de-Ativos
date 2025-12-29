using Ativos.Application.UseCases.Login.DoLogin;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
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
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();
        
        var request =  RequestLoginJsonBuilder.Build();
        request.Matricula = -123;
        
        var useCase = CreateUseCase(user, request.Matricula);
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("User not found"));
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Password = "senhaErrada123"; // Senha diferente
        
        var useCase = CreateUseCaseWithWrongPassword(user);
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Matricula ou senha invalidas"));
    }

    private DoLoginUseCase CreateUseCase(Usuario usuario, long? matricula = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();
        var mapper = MapperBuilder.Build();

        if (matricula != null)
        {
            var matriculaToUse = matricula ?? usuario.Matricula;
            readRepository.GetUserByMatriculaNotFound(matriculaToUse);
        }
        else readRepository.GetUserByMatricula(usuario);
        
        return new DoLoginUseCase(readRepository.Build(), passwordEncripter, tokenGenerator, mapper);
    }

    private DoLoginUseCase CreateUseCaseWithWrongPassword(Usuario usuario)
    {
        var passwordEncripter = PasswordEncripterBuilder.BuildWithWrongPassword();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByMatricula(usuario).Build();
        var mapper = MapperBuilder.Build();
        
        return new DoLoginUseCase(readRepository, passwordEncripter, tokenGenerator, mapper);
    }
}
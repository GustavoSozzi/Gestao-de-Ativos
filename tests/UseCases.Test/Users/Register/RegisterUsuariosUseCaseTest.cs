using Ativos.Application.UseCases.Register.Usuarios;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Register;

public class RegisterUsuariosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
        result.P_nome.Should().Be(request.P_nome); //deve ser igual ao nome da request
        result.Sobrenome.Should().Be(request.Sobrenome); //deve ser igual ao sobrenome da request
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    private RegisterUsuariosUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().Build();

        return new RegisterUsuariosUseCase(writeRepository, tokenGenerator, readRepository, passwordEncripter, unitOfWork,  mapper);
    }
}
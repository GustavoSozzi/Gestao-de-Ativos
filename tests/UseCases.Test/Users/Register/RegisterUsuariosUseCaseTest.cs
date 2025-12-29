using Ativos.Application.UseCases.Register.Usuarios;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
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

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();
        request.P_nome = string.Empty;
        
        var useCase = CreateUseCase();
        
        var act = async () => await useCase.Execute(request);
        
        var result =  await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_REQUIRED)); //espera 1 erro especifico
    }

    [Fact]
    public async Task Error_Matricula_Ja_Existe()
    {
        var request = RequestRegisterUsuariosJsonBuilder.Build();

        var useCase = CreateUseCase(request.Matricula);
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Matricula ja registrada"));
    }

    private RegisterUsuariosUseCase CreateUseCase(long? matricula = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();

        if (matricula is long m) {readRepository.ExistActiveUserMatricula(m);} //so sera mockado se houver matricula como parametro

        return new RegisterUsuariosUseCase(writeRepository, tokenGenerator, readRepository.Build(), passwordEncripter, unitOfWork,  mapper);
    }
}
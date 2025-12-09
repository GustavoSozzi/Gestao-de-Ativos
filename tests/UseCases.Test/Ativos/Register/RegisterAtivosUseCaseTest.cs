using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Usuarios;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Ativos.Register;

public class RegisterAtivosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterAtivosJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
        result.Nome.Should().Be(request.Nome); //deve ser igual ao nome da request
        result.Modelo.Should().Be(request.Modelo); //deve ser igual ao modelo da request
        result.SerialNumber.Should().Be(request.SerialNumber);
    }
    
    private RegisterAtivosUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = AtivosWriteOnlyRepositoryBuilder.Build();

        return new RegisterAtivosUseCase(writeRepository, unitOfWork, mapper);
    }
}
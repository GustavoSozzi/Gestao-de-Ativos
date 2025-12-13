using Ativos.Application.UseCases.Register.Licencas;
using Ativos.Application.UseCases.Register.Localizacao;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace UseCases.Test.Localizacao;

public class RegisterLocalizacaoUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterLocalizacaoJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
    }
    
    private RegisterLocalizacaoUseCase CreateUseCase()
    {
        var writeRepository = LocalizacaoWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();

        return new RegisterLocalizacaoUseCase(writeRepository, unitOfWork, mapper);
    }
}
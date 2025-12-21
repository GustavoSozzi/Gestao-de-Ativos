using Ativos.Application.UseCases.Register.Licencas;
using Ativos.Application.UseCases.Register.Localizacao;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
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

    [Fact]
    public async Task Error_City_NotFound()
    {
        var request = RequestRegisterLocalizacaoJsonBuilder.Build();
        request.Cidade = string.Empty;
        
        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    [Fact]
    public async Task Error_State_NotFound()
    {
        var request = RequestRegisterLocalizacaoJsonBuilder.Build();
        request.Estado = string.Empty;
        
        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.FIELD_REQUIRED));
    }
    
    private RegisterLocalizacaoUseCase CreateUseCase()
    {
        var writeRepository = LocalizacaoWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();

        return new RegisterLocalizacaoUseCase(writeRepository, unitOfWork, mapper);
    }
}
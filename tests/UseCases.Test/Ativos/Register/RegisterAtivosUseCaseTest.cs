using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace UseCases.Test.Ativos.Register;

public class RegisterAtivosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        
        var request = RequestRegisterAtivosJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
        result.Nome.Should().Be(request.Nome);
        result.Modelo.Should().Be(request.Modelo); 
        result.SerialNumber.Should().Be(request.SerialNumber);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var loggedUser = UserBuilder.Build();
        
        var request = RequestRegisterAtivosJsonBuilder.Build();
        request.Nome = string.Empty;
        
        var useCase = CreateUseCase(loggedUser);
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_REQUIRED));
    }
    
    private RegisterAtivosUseCase CreateUseCase(Usuario usuario)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = AtivosWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);

        return new RegisterAtivosUseCase(writeRepository, unitOfWork, mapper, loggedUser);
    }
}
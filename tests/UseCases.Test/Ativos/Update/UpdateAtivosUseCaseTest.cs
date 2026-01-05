using Ativos.Application.UseCases.Update;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Update;
using FluentAssertions;

namespace UseCases.Test.Ativos.Update;

public class UpdateAtivosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestUpdateAtivosJsonBuilder.Build();
        var ativos = AtivosBuilder.Build(loggedUser);
        
        var useCase = CreateUseCase(loggedUser, ativos);

        var act = async () => await useCase.Execute(ativos.Id_ativo, request);

        await act.Should().NotThrowAsync();
        
        ativos.Nome.Should().Be(request.Nome);
        ativos.Modelo.Should().Be(request.Modelo);
        ativos.SerialNumber.Should().Be(request.SerialNumber);
        ativos.CodInventario.Should().Be(request.CodInventario);
        ativos.Tipo.Should().Be(request.Tipo);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var ativos = AtivosBuilder.Build(loggedUser);
        
        var request = RequestUpdateAtivosJsonBuilder.Build();
        request.Nome = string.Empty;
        
        var useCase = CreateUseCase(loggedUser, ativos);
        
        var act = async () => await useCase.Execute(ativos.Id_ativo,  request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Fact]
    public async Task Error_Ativo_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        
        var request = RequestUpdateAtivosJsonBuilder.Build();
        
        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 10000, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }

    private UpdateAtivosUseCase CreateUseCase(Usuario usuario, Ativo ativo = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateRepository = new AtivosUpdateOnlyRepositoryBuilder().GetById(usuario, ativo).Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new UpdateAtivosUseCase(mapper, unitOfWork, updateRepository, loggedUser);
    }
}
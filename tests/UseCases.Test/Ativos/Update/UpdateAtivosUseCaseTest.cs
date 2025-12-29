using Ativos.Application.UseCases.Update;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
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
        var ativos = AtivosBuilder.Build();
        var request = RequestUpdateAtivosJsonBuilder.Build();
        var useCase = CreateUseCase(ativos, ativos.Id_ativo);

        await useCase.Execute(ativos.Id_ativo, request); //nao lancou excecao = passou no teste
        
        ativos.CodInventario.Should().Be(request.CodInventario); //deve ser igual ao da request
        //Arrange, Act, Assert (Organizar, Agir, Asserir):
    }

    [Fact]
    public async Task Error_Ativo_Not_Found()
    {
        var ativos = AtivosBuilder.Build();
        var request = RequestUpdateAtivosJsonBuilder.Build();
        
        var useCase = CreateUseCase(ativos);

        var act = async () => await useCase.Execute(ativos.Id_ativo, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }

    private UpdateAtivosUseCase CreateUseCase(Ativo ativos, long? Id_ativo = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateRepository = new AtivosUpdateOnlyRepositoryBuilder();

        if (Id_ativo.HasValue) updateRepository.GetById(ativos);
        
        return new UpdateAtivosUseCase(mapper, unitOfWork, updateRepository.Build());
    }
}
using Ativos.Application.UseCases.Update.Chamados;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace UseCases.Test.Chamados.Update;

public class UpdateChamadosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var chamados = ChamadosBuilder.Build();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        var useCase = CreateUseCase(chamados, chamados.Id_Chamado);
        
        await useCase.Execute(chamados.Id_Chamado, request);

        chamados.Data_Abertura.Should().Be(request.Data_Abertura);
        chamados.Titulo.Should().Be(request.Titulo);
        chamados.Descricao.Should().Be(request.Descricao);
        chamados.Solucao.Should().Be(request.Solucao);
    }
    
    [Fact]
    public async Task Error_Called_Not_Found()
    {
        var chamados = ChamadosBuilder.Build();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        
        var useCase = CreateUseCase(chamados);

        var act = async () => await useCase.Execute(chamados.Id_Chamado, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("NOT FOUND"));
    }
    
    
    private UpdateChamadosUseCase CreateUseCase(Chamado chamado, long? Id_chamado = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateRepository = new ChamadosUpdateOnlyRepositoryBuilder();
        
        if (Id_chamado.HasValue) updateRepository.GetById(chamado);
        
        return new UpdateChamadosUseCase(mapper, unitOfWork, updateRepository.Build());

    }
}
using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace UseCases.Test.Chamados.Register;

public class RegisterChamadosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var chamados = ChamadosBuilder.Build();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        var useCase = CreateUseCase(chamados);
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Error_Ativo_Not_Found()
    {
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Id_Ativo = -1;
        
        var useCase = CreateUseCaseWithoutAtivos();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Ativo não encontrado"));
    }
    
    private RegisterChamadosUseCase CreateUseCase(Chamado chamado)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = ChamadosWriteOnlyRepositoryBuilder.Build();
        var readRepository = new ChamadosReadOnlyRepositoryBuilder().GetById(chamado);
        
        return new RegisterChamadosUseCase(writeRepository, readRepository.Build(), unitOfWork, mapper);
    }
    
    private RegisterChamadosUseCase CreateUseCaseWithoutAtivos()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = ChamadosWriteOnlyRepositoryBuilder.Build();
        var readRepository = new ChamadosReadOnlyRepositoryBuilder();
        
        return new RegisterChamadosUseCase(writeRepository, readRepository.Build(), unitOfWork, mapper);
    }
}
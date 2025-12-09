using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
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
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Id_Ativo = 19;
        
        var useCase = CreateUseCase(request);
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
    }
    
    private RegisterChamadosUseCase CreateUseCase(RequestChamadosJson request)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = ChamadosWriteOnlyRepositoryBuilder.Build();
        var readRepository = new ChamadosReadOnlyRepositoryBuilder()
            .WithGetByIdReturning(
                request.Id_Ativo,
                new Ativo { Id_ativo = request.Id_Ativo, Nome = "Notebook Dell" }
            )
            .Build();

        return new RegisterChamadosUseCase(writeRepository, readRepository, unitOfWork, mapper);
    }
}
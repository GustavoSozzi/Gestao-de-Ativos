using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Application.UseCases.Register.Licencas;
using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace UseCases.Test.Licencas.Register;

public class RegisterLicencasUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterLicencasJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
    }
    
    private RegisterLicencasUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = ChamadosWriteOnlyRepositoryBuilder.Build();
        var readRepository = new ChamadosReadOnlyRepositoryBuilder();

        return new RegisterLicencasUseCase(null, null, null, null);
    }
}
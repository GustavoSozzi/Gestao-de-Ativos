using Ativos.Application.UseCases.Register.Ativos;
using Ativos.Application.UseCases.Register.Chamados;
using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
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
        var loggedUser = UserBuilder.Build();
        
        var chamados = ChamadosBuilder.Build();
        var request = RequestRegisterChamadosJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser, chamados);
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Error_Ativo_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        
        var request = RequestRegisterChamadosJsonBuilder.Build();
        request.Id_Ativo = -1;
        
        var useCase = CreateUseCaseWithoutAtivos(loggedUser);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Ativo não encontrado"));
    }
    
    private RegisterChamadosUseCase CreateUseCase(Usuario usuario, Chamado chamado)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        var writeRepository = ChamadosWriteOnlyRepositoryBuilder.Build();
        var readRepository = new ChamadosReadOnlyRepositoryBuilder().GetById(usuario, chamado);
        
        return new RegisterChamadosUseCase(writeRepository, readRepository.Build(), loggedUser, unitOfWork, mapper);
    }
    
    private RegisterChamadosUseCase CreateUseCaseWithoutAtivos(Usuario usuario)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        var writeRepository = ChamadosWriteOnlyRepositoryBuilder.Build();
        var readRepository = new ChamadosReadOnlyRepositoryBuilder();
        
        return new RegisterChamadosUseCase(writeRepository, readRepository.Build(), loggedUser, unitOfWork, mapper);
    }
}
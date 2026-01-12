using Ativos.Application.UseCases.GetAll.Chamados;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Chamados.GetAll;

public class GetAllChamadosUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var ativos = AtivosBuilder.Build(loggedUser);
        var chamados = ChamadosBuilder.Collection(ativos);
        
        var useCase = CreateUseCase(chamados, loggedUser);
        
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Chamados.Should().NotBeNullOrEmpty().And.AllSatisfy(chamado =>
        {
            chamado.Id_Chamado.Should().BeGreaterThan(0);
            chamado.Id_Ativo.Should().BeGreaterThan(0);
            chamado.Data_Abertura.Should().NotBe(default);
            chamado.Titulo.Should().NotBeNullOrEmpty();
            chamado.Descricao.Should().NotBeNullOrEmpty();
            chamado.Status_Chamado.Should().BeDefined();
        });
    }
    
    private GetAllChamadosUseCase CreateUseCase(List<Chamado> chamados, Usuario usuario)
    {
        var repository = new ChamadosRepositoryBuilder().GetAll(usuario, chamados).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new GetAllChamadosUseCase(repository, loggedUser, mapper);
    }
}
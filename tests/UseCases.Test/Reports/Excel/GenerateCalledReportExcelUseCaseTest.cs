using Ativos.Application.UseCases.Reports.Excel;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Reports.Excel;

public class GenerateCalledReportExcelUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var ativos = AtivosBuilder.Build(loggedUser);
        var chamados = ChamadosBuilder.Collection(ativos);
        
        var useCase = CreateUseCase(loggedUser, chamados);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuilder.Build();
        
        var useCase = CreateUseCase(loggedUser, new List<Chamado>());

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }
    

    private GenerateChamadosReportExcelUseCase CreateUseCase(Usuario usuario, List<Chamado> chamados)
    {
        var repository = new ChamadosRepositoryBuilder().FilterByMonth(usuario, chamados).Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new GenerateChamadosReportExcelUseCase(repository, loggedUser);
    }
}
using Ativos.Application.UseCases.GetAll.Licencas;
using Ativos.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.UsersLicense.GetAll;

public class GetAllLicenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        
        var licenca1 = LicenceBuilder.BuildWithId(1);
        var licenca2 = LicenceBuilder.BuildWithId(2);
        var licenca3 = LicenceBuilder.BuildWithId(3);
        
        var usersLicenses = new List<Licenca> {licenca1, licenca2, licenca3};
        
        var useCase = CreateUseCase(loggedUser, usersLicenses);
        
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Licencas.Should().NotBeNullOrEmpty()
            .And.HaveCount(3)
            .And.AllSatisfy(licenca =>
            {
                licenca.Id_Licenca.Should().BeGreaterThan(0);
                licenca.Tipo_Licenca.Should().BeDefined();
                licenca.Data.Should().BeAfter(DateTime.MinValue);
            });

        result.Licencas.Should().Contain(l => l.Id_Licenca == licenca1.Id_Licenca);
        result.Licencas.Should().Contain(l => l.Id_Licenca == licenca2.Id_Licenca);
        result.Licencas.Should().Contain(l => l.Id_Licenca == licenca3.Id_Licenca);
    }
    
    private GetAllLicencasUseCase CreateUseCase(Usuario usuario, List<Licenca> licencas)
    {
        var repository = new LicenseReadOnlyRepositoryBuilder().GetAll(usuario, licencas).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        
        return new GetAllLicencasUseCase(repository, loggedUser, mapper);
    }
}
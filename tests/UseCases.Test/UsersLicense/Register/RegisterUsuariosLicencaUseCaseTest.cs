using Ativos.Application.UseCases.Register.Usuarios;
using Ativos.Domain.Entities;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;

namespace UseCases.Test.UsersLicense.Register;

public class RegisterUsuariosLicencaUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var licence = LicenceBuilder.BuildWithId(3); // Mesmo ID da requisição
        var request = RequestVincularLicencaJsonBuilder.Build();
        var useCase = CreateUseCase(user, licence);
        
        var result = await useCase.Execute(request);
        
        result.Should().NotBeNull();
        result.Id_Usuario.Should().Be(request.Id_Usuario);
        result.Ids_Licencas.Should().BeEquivalentTo(request.Ids_Licencas);
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();
        var licence = LicenceBuilder.BuildWithId(3);
        var request = RequestVincularLicencaJsonBuilder.Build();
        request.Id_Usuario = 999999;
        
        var useCase = CreateUseCase(user, licence);
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("User not found"));
    }

    [Fact]
    public async Task Error_Licence_Not_Found()
    {
        var user = UserBuilder.Build();
        var request = RequestVincularLicencaJsonBuilder.Build();
        request.Ids_Licencas = new List<long> { 999999 }; 

        long idLicenca = 999999;
        
        // Não mockar a licença com ID 999999 para simular "não encontrada"
        var useCase = CreateUseCaseWithoutLicence(user); 
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains($"license {idLicenca} not found"));
    }

    private RegisterUsuariosLicencasUseCase CreateUseCase(Usuario usuario, Licenca licenca)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userReadRepository = new UserReadOnlyRepositoryBuilder().GetById(usuario).Build();
        var licenceReadRepository = new LicenseReadOnlyRepositoryBuilder().GetById(licenca).Build();
        
        return new RegisterUsuariosLicencasUseCase(userReadRepository, licenceReadRepository, unitOfWork, mapper);
    }

    private RegisterUsuariosLicencasUseCase CreateUseCaseWithoutLicence(Usuario usuario)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userReadRepository = new UserReadOnlyRepositoryBuilder().GetById(usuario).Build();
        var licenceReadRepository = new LicenseReadOnlyRepositoryBuilder().Build(); 
        
        return new RegisterUsuariosLicencasUseCase(userReadRepository, licenceReadRepository, unitOfWork, mapper);
    }
}
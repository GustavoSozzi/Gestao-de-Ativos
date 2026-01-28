using Ativos.Application.UseCases.Users.ChangePassword;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Users.ChangePasswordUseCaseTest;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var user = UserBuilder.Build();
        
        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = string.Empty;
        
        var useCase = CreateUseCase(user, request.Password);
        
        var act = async () => { await useCase.Execute(request); };

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count == 1 && e.GetErrors().Contains(ResourceErrorMessages.INVALID_PASSWORD));
    }
    
    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        var user = UserBuilder.Build();
        
        var request = RequestChangePasswordJsonBuilder.Build();
        
        var useCase = CreateUseCase(user);
        
        var act = async () => { await useCase.Execute(request); };

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count == 1 && e.GetErrors().Contains(ResourceErrorMessages.INVALID_PASSWORD));
    }

    public static ChangePasswordUseCase CreateUseCase(Usuario usuario, string? password = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userUpdateRepository = UsuariosUpdateOnlyRepositoryBuilder.Build().WithUser(usuario).BuildRepository();
        var loggedUser = LoggedUserBuilder.Build(usuario);
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Builder();
        
        return new ChangePasswordUseCase(loggedUser, passwordEncripter, userUpdateRepository, unitOfWork);
    }
}
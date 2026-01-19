using Ativos.Communication.Requests;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Domain.Security.Cryptography;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace Ativos.Application.UseCases.Users.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUsuariosUpdateOnlyReposiitory _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IPasswordEncripter passwordEncripter,
        IUsuariosUpdateOnlyReposiitory repository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();
        
        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id_usuario);
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);
        
        _repository.Update(user);
        
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Usuario loggedUser)
    {
        var validator = new ChangePasswordValidator();
        
        var result = validator.Validate(request);

        var passwordMatch = _passwordEncripter.Verify(request.Password, loggedUser.Password);
        
        if(!passwordMatch) result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.INVALID_PASSWORD));
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        } 
    }
}
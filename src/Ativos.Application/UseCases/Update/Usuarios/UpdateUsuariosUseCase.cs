using Ativos.Communication.Requests;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;
using FluentValidation.Results;

namespace Ativos.Application.UseCases.Update.Usuarios;

public class UpdateUsuariosUseCase : IUpdateUsuariosUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IUsuariosUpdateOnlyReposiitory _repository;
    private readonly IUsuariosReadOnlyRepository _readOnlyRepository;
    
    public UpdateUsuariosUseCase(IUnitOfWork unitOfWork, ILoggedUser loggedUser, IUsuariosReadOnlyRepository usuariosReadOnlyRepository, IUsuariosUpdateOnlyReposiitory repository)
    {
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _repository = repository;
        _readOnlyRepository = usuariosReadOnlyRepository;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();
        
        await Validate(request, loggedUser.Matricula);
        
        var usuario = await _repository.GetById(loggedUser.Id_usuario);
        
        if(usuario is null) throw new NotFoundException("NOT FOUND");

        usuario.P_nome = request.P_Nome;
        usuario.Sobrenome = request.Sobrenome;
        if (request.Matricula != 0) usuario.Matricula = request.Matricula;
        
        _repository.Update(usuario);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, long currentMatricula)
    {
        var validator = new UpdateUsuariosValidator();

        var result = validator.Validate(request);
    
        if (currentMatricula != request.Matricula)
        {
            if (request.Matricula != 0)
            {
                var userExist = await _readOnlyRepository.ExistActiveUserMatricula(request.Matricula);

                if (userExist) result.Errors.Add(new ValidationFailure(nameof(request.Matricula), "Matrícula já existe"));
            }
        }

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Register.Ativos;

public class RegisterAtivosUseCase : IRegisterAtivosUseCase
{
    private readonly IAtivosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public RegisterAtivosUseCase(IAtivosWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterAtivosJson> Execute(RequestAtivosJson request)
    {
        ValidateAtivos(request);

        var loggedUser = await _loggedUser.Get();
        
        var entity = _mapper.Map<Ativo>(request);
        entity.id_usuario = loggedUser.Id_usuario;
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterAtivosJson>(entity);
    }

    private void ValidateAtivos(RequestAtivosJson request)
    {
        var validator = new AtivosValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
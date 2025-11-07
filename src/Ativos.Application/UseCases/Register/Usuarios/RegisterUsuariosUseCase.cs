using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Register.Usuarios;

public class RegisterUsuariosUseCase : IRegisterUsuariosUseCase
{
    private readonly IUsuariosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUsuariosUseCase(IUsuariosWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterUsuariosJson> Execute(RequestUsuariosJson request)
    {
        ValidateUsuarios(request);

        var entity = _mapper.Map<Usuario>(request);
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterUsuariosJson>(entity);
    }

    private void ValidateUsuarios(RequestUsuariosJson request)
    {
        var validator = new UsuariosValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
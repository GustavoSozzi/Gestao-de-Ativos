using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Licencas;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Application.UseCases.Register.Licencas;

public class RegisterLicencasUseCase : IRegisterLicencasUseCase
{
    private readonly ILicencasWriteOnlyRepository _repository;
    private readonly IUsuariosReadOnlyRepository _usuariosRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterLicencasUseCase(
        ILicencasWriteOnlyRepository repository,
        IUsuariosReadOnlyRepository usuariosRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _usuariosRepository = usuariosRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterLicencasJson> Execute(RequestLicencasJson request)
    {
        ValidateLicencas(request);

        var entity = _mapper.Map<Licenca>(request);
        
        // Se houver usuário para vincular, usa o método específico
        if (request.id_usuario > 0)
        {
            await _repository.AddWithUsuario(entity, request.id_usuario);
        }
        else
        {
            await _repository.Add(entity);
        }
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterLicencasJson>(entity);
    }

    private void ValidateLicencas(RequestLicencasJson request)
    {
        var validator = new LicencasValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
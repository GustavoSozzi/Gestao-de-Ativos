using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Contratos;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Register.Contratos;

public class RegisterContratosUseCase : IRegisterContratosUseCase
{
    
    private readonly IContratosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterContratosUseCase(IContratosWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterContratosJson> Execute(RequestContratosJson request)
    {
        ValidateContratos(request);

        var entity = _mapper.Map<Contrato>(request);
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterContratosJson>(entity);
    }

    private void ValidateContratos(RequestContratosJson request)
    {
        var validator = new ContratosValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
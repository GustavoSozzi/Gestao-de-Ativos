using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Register.Chamados;

public class RegisterChamadosUseCase : IRegisterChamadosUseCase
{
    private readonly IChamadosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterChamadosUseCase(IChamadosWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterChamadosJson> Execute(RequestChamadosJson request)
    {
        ValidateChamados(request);

        var entity = _mapper.Map<Chamado>(request);
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterChamadosJson>(entity);
    }

    private void ValidateChamados(RequestChamadosJson request)
    {
        var validator = new ChamadosValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
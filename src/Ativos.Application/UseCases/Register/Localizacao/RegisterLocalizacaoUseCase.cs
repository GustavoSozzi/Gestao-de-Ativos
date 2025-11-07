using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Repositories.Localizacao;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Register.Localizacao;

public class RegisterLocalizacaoUseCase : IRegisterLocalizacaoUseCase
{
    private readonly ILocalizacaoWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterLocalizacaoUseCase(ILocalizacaoWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterLocalizacaoJson> Execute(RequestLocalizacaoJson request)
    {
        ValidateLocalizacao(request);

        var entity = _mapper.Map<Domain.Entities.Localizacao>(request);
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterLocalizacaoJson>(entity);
    }

    private void ValidateLocalizacao(RequestLocalizacaoJson request)
    {
        var validator = new LocalizacaoValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
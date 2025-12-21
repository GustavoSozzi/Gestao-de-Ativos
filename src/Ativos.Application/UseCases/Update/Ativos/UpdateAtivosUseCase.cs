using Ativos.Communication.Requests;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Update;

public class UpdateAtivosUseCase : IUpdateAtivosUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAtivosUpdateOnlyRepository _repository;
    
    public UpdateAtivosUseCase(IMapper mapper, IUnitOfWork unitOfWork, IAtivosUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Execute(long id, RequestAtivosJson request)
    {
        Validate(request);

        var ativo = await _repository.GetById(id);

        if(ativo is null) throw new NotFoundException("NOT FOUND");

        _mapper.Map(request, ativo);

        _repository.Update(ativo);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestAtivosJson request)
    {
        var validator = new AtivosValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
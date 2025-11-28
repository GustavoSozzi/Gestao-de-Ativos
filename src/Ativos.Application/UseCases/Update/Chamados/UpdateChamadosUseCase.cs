using Ativos.Communication.Requests;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Update.Chamados;

public class UpdateChamadosUseCase : IUpdateChamadosUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChamadosUpdateOnlyRepository _repository;
    
    public UpdateChamadosUseCase(IMapper mapper, IUnitOfWork unitOfWork, IChamadosUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    
    public async Task Execute(long id, RequestChamadosJson request)
    {
        Validate(request);

        var chamado = await _repository.GetById(id);

        if(chamado is null) { throw new NotFoundException("NOT FOUND");}

        _mapper.Map(request, chamado);

        _repository.Update(chamado);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChamadosJson request)
    {
        var validator = new ChamadosValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
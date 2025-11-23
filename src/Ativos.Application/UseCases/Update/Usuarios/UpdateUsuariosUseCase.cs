using Ativos.Communication.Requests;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Update.Usuarios;

public class UpdateUsuariosUseCase : IUpdateUsuariosUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuariosUpdateOnlyReposiitory _repository;
    
    public UpdateUsuariosUseCase(IMapper mapper, IUnitOfWork unitOfWork, IUsuariosUpdateOnlyReposiitory repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Execute(long id, RequestUsuariosJson request)
    {
        Validate(request);

        var usuario = await _repository.GetById(id);

        if(usuario is null)
        {
            throw new NotFoundException("NOT FOUND");
        }

        _mapper.Map(request, usuario);

        _repository.Update(usuario);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestUsuariosJson request)
    {
        var validator = new UsuariosValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
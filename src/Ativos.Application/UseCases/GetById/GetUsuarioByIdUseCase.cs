using Ativos.Communication.responses.Usuarios;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Exception;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.GetById;

public class GetUsuarioByIdUseCase : IGetUsuarioByIdUseCase
{
    private readonly IUsuariosReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetUsuarioByIdUseCase(IUsuariosReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseUsuarioJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if(result is null)
            throw new NotFoundException("NOT FOUND");
        
        return _mapper.Map<ResponseUsuarioJson>(result);
    }
}
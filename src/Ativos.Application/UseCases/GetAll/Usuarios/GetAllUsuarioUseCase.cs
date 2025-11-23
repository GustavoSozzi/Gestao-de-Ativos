using Ativos.Communication.responses.Usuarios;
using Ativos.Domain.Repositories.Usuarios;
using AutoMapper;

namespace Ativos.Application.UseCases.GetAll.Usuarios;

public class GetAllUsuarioUseCase : IGetAllUsuarioUseCase
{
    private readonly IUsuariosReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllUsuarioUseCase(IUsuariosReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseUsuariosJson> Execute(long? matricula = null, string? nome = null, 
        string? departamento = null, string? cargo = null, string? role = null)
    {
        var result = await _repository.GetAll(matricula, nome, departamento, cargo, role);

        return new ResponseUsuariosJson()
        {
            Usuarios = _mapper.Map<List<ResponseShortUsuarioJson>>(result)
        };
    }
}
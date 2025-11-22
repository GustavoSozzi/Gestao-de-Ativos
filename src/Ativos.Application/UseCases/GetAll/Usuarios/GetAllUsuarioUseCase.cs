using Ativos.Communication.responses;
using Ativos.Communication.responses.Usuarios;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Localizacao;
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

    public async Task<ResponseUsuariosJson> Execute(long? matricula = null)
    {
        if (matricula.HasValue)
        {
            var usuario = await _repository.GetUserByMatricula((int)matricula.Value);
            var usuarios = usuario != null ? new List<Usuario> { usuario } : new List<Usuario>();
            
            return new ResponseUsuariosJson()
            {
                Usuarios = _mapper.Map<List<ResponseShortUsuarioJson>>(usuarios)
            };
        }
        
        var result = await _repository.GetAll();

        return new ResponseUsuariosJson()
        {
            Usuarios = _mapper.Map<List<ResponseShortUsuarioJson>>(result)
        };
    }
}
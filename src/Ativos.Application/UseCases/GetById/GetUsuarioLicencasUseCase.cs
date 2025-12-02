using Ativos.Domain.Repositories.Usuarios;
using Ativos.Exception.ExceptionsBase;

namespace Ativos.Application.UseCases.GetById;

public class GetUsuarioLicencasUseCase : IGetUsuarioLicencasUseCase
{
    private readonly IUsuariosReadOnlyRepository _repository;

    public GetUsuarioLicencasUseCase(IUsuariosReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<long>> Execute(long idUsuario)
    {
        var usuario = await _repository.GetById(idUsuario);

        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado");

        return usuario.licencas?.Select(l => l.Id_Licenca).ToList() ?? new List<long>();
    }
}

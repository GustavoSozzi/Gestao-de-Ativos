using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Usuarios;

public interface IUsuariosWriteOnlyRepository
{
    public Task Add(Usuario usuario);
    Task<bool> Delete(long id);
}
using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Usuarios;

public interface IUsuariosUpdateOnlyReposiitory
{
    Task<Usuario> GetById(long id);
    void Update(Usuario usuario);
}
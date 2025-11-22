namespace Ativos.Domain.Repositories.Usuarios;

public interface IUsuariosReadOnlyRepository
{
    Task<List<Entities.Usuario>> GetAll();
    Task<bool> ExistActiveUserMatricula(int matricula);
    Task<Entities.Usuario?> GetUserByMatricula(int matricula);
}
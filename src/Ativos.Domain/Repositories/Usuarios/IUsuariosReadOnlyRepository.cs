namespace Ativos.Domain.Repositories.Usuarios;

public interface IUsuariosReadOnlyRepository
{
    Task<List<Entities.Usuario>> GetAll(long? matricula = null, string? nome = null, 
        string? departamento = null, string? cargo = null, string? role = null);
    Task<bool> ExistActiveUserMatricula(long matricula);
    Task<Entities.Usuario?> GetUserByMatricula(long matricula);
    Task<Entities.Usuario?> GetById(long id);

}
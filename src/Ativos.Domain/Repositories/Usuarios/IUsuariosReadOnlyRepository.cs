namespace Ativos.Domain.Repositories.Usuarios;

public interface IUsuariosReadOnlyRepository
{
    Task<bool> ExistActiveUserMatricula(int matricula);
}
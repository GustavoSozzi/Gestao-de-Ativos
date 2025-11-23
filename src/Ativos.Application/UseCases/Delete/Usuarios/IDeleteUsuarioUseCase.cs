namespace Ativos.Application.UseCases.Delete.Usuarios;

public interface IDeleteUsuarioUseCase
{
    Task Execute(long id);
}

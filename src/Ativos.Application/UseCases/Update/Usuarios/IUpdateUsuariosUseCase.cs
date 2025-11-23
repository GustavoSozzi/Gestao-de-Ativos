using Ativos.Communication.Requests;

namespace Ativos.Application.UseCases.Update.Usuarios;

public interface IUpdateUsuariosUseCase
{
    Task Execute(long id, RequestUsuariosJson request);
}
using Ativos.Communication.responses.Usuarios;

namespace Ativos.Application.UseCases.GetAll.Usuarios;

public interface IGetAllUsuarioUseCase
{
    Task<ResponseUsuariosJson> Execute(long? matricula = null);
}
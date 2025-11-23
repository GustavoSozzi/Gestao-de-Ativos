using Ativos.Communication.responses.Usuarios;

namespace Ativos.Application.UseCases.GetById;

public interface IGetUsuarioByIdUseCase
{
    Task<ResponseUsuarioJson> Execute(long id);
}
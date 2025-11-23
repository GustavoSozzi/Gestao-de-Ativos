using Ativos.Communication.responses.Usuarios;

namespace Ativos.Application.UseCases.GetAll.Usuarios;

public interface IGetAllUsuarioUseCase
{
    Task<ResponseUsuariosJson> Execute(long? matricula = null, string? nome = null, 
        string? departamento = null, string? cargo = null, string? role = null);
}
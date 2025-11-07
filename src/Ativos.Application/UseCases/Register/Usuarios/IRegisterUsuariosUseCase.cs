using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Usuarios;

public interface IRegisterUsuariosUseCase
{
    Task <ResponseRegisterUsuariosJson> Execute(RequestUsuariosJson request);
}
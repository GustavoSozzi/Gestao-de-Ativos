using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Usuarios;

public interface IRegisterUsuariosLicencasUseCase
{
    Task <ResponseRegisterUsuariosLicencasJson> Execute(RequestVincularLicencaJson request);
}
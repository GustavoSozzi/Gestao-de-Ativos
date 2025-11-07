using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Licencas;

public interface IRegisterLicencasUseCase
{
    Task <ResponseRegisterLicencasJson> Execute(RequestLicencasJson request);
}
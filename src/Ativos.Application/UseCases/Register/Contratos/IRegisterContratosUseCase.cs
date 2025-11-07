using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Contratos;

public interface IRegisterContratosUseCase
{
    Task <ResponseRegisterContratosJson> Execute(RequestContratosJson request);
}
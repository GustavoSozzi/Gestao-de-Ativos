using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Chamados;

public interface IRegisterChamadosUseCase
{
    Task <ResponseRegisterChamadosJson> Execute(RequestChamadosJson request);
}
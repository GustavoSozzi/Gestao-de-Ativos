using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Ativos;

public interface IRegisterAtivosUseCase
{
    Task <ResponseRegisterAtivosJson> Execute(RequestAtivosJson request);
}
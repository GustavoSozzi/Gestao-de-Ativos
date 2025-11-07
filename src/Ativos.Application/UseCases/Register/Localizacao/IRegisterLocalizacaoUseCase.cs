using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;

namespace Ativos.Application.UseCases.Register.Localizacao;

public interface IRegisterLocalizacaoUseCase
{
    Task <ResponseRegisterLocalizacaoJson> Execute(RequestLocalizacaoJson request);
}
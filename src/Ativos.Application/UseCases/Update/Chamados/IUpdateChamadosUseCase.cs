using Ativos.Communication.Requests;

namespace Ativos.Application.UseCases.Update.Chamados;

public interface IUpdateChamadosUseCase
{
    Task Execute(long id, RequestChamadosJson request);

}
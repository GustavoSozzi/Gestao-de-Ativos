using Ativos.Communication.responses.Chamados;

namespace Ativos.Application.UseCases.GetAll.Chamados;

public interface IGetAllChamadosUseCase
{
    Task<ResponseChamadosJson> Execute();
}
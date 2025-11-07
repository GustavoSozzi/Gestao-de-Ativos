using Ativos.Communication.responses;

namespace Ativos.Application.UseCases.GetAll;

public interface IGetAllAtivosUseCase
{
    Task<ResponseAtivosJson> Execute();
}
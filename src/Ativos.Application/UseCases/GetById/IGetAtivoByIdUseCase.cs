using Ativos.Communication.responses;

namespace Ativos.Application.UseCases.GetById;

public interface IGetAtivoByIdUseCase
{
    Task<ResponseAtivoJson> Execute(long id);
}
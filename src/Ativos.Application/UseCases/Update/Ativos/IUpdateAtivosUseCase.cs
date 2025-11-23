using Ativos.Communication.Requests;

namespace Ativos.Application.UseCases.Update;

public interface IUpdateAtivosUseCase
{
    Task Execute(long id, RequestAtivosJson request);
}
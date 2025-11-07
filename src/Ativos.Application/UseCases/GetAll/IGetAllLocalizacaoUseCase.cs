using Ativos.Communication.responses;

namespace Ativos.Application.UseCases.GetAll;

public interface IGetAllLocalizacaoUseCase
{
    Task<ResponseLocalizacaoJson> Execute();
}